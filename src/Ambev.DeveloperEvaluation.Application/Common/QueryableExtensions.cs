using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.Application.Common
{
    public static class QueryableExtensions
    {
        private static readonly Type[] NumericTypes = { typeof(int), typeof(double), typeof(decimal) };

        public static IQueryable<T> ApplyDynamicFilters<T>(
            this IQueryable<T> query,
            IDictionary<string, string> filters,
            IEnumerable<string>? allowed = null)
        {
            if (filters == null || filters.Count == 0)
                return query;

            var propertyMap = BuildPropertyMap(typeof(T));
            var allowedSet = allowed?.ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var filter in filters)
            {
                if (TryParseFilter(filter.Key, filter.Value, propertyMap, allowedSet, out var expr, out var value))
                {
                    query = query.Where(expr, value);
                }
            }

            return query;
        }

        private static Dictionary<string, (string Path, Type Type)> BuildPropertyMap(Type type)
        {
            var map = new Dictionary<string, (string, Type)>(StringComparer.OrdinalIgnoreCase);
            void Recurse(Type current, string prefix)
            {
                foreach (var prop in current.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var path = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}.{prop.Name}";
                    var t = prop.PropertyType;

                    if (t == typeof(string) || NumericTypes.Contains(t) || t == typeof(DateTime))
                    {
                        map[prop.Name] = (path, t);
                    }
                    else if (t.IsClass && t != typeof(string))
                    {
                        Recurse(t, path);
                    }
                }
            }

            Recurse(type, "");
            return map;
        }

        private static bool TryParseFilter(
            string rawKey,
            string rawVal,
            Dictionary<string, (string Path, Type Type)> map,
            HashSet<string>? allowed,
            out string expression,
            out object? value)
        {
            expression = string.Empty;
            value = null;

            var val = rawVal?.Trim();
            if (string.IsNullOrEmpty(val))
                return false;

            var (leaf, op) = ParseOperator(rawKey);
            if (allowed != null && !allowed.Contains(leaf))
                return false;

            if (!map.TryGetValue(leaf, out var info))
                return false;

            var (path, type) = info;
            if (type == typeof(string))
            {
                expression = BuildStringExpression(path, val, out var cleaned);
                value = cleaned;
                return expression != null;
            }

            if (NumericTypes.Contains(type) || type == typeof(DateTime))
            {
                value = Convert.ChangeType(val, type, CultureInfo.InvariantCulture)!;
                expression = $"{path} {op} @0";
                return true;
            }

            return false;
        }

        private static (string Leaf, string Op) ParseOperator(string key)
        {
            if (key.StartsWith("_min", StringComparison.OrdinalIgnoreCase))
                return (key[4..], ">=");
            if (key.StartsWith("_max", StringComparison.OrdinalIgnoreCase))
                return (key[4..], "<=");
            return (key, "==");
        }

        private static string BuildStringExpression(string path, string val, out string cleaned)
        {
            cleaned = val.Trim('*');
            var startsWith = val.EndsWith("*");
            var endsWith = val.StartsWith("*");

            return startsWith && endsWith
                ? $"{path}.Contains(@0)"
                : endsWith
                    ? $"{path}.StartsWith(@0)"
                    : startsWith
                        ? $"{path}.EndsWith(@0)"
                        : $"{path} == @0";
        }

        public static IEnumerable<string> GetPropertyNames(this Type type)
        {
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var t = prop.PropertyType;
                if (t == typeof(string) || t == typeof(bool) || t == typeof(DateTime) || NumericTypes.Contains(t))
                {
                    yield return prop.Name;
                }
                else if (t.IsClass && t != typeof(string))
                {
                    foreach (var child in t.GetPropertyNames())
                        yield return child;
                }
            }
        }
    }
}
