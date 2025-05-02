using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.Application.Common
{
    /// <summary>
    /// Applies dynamic query-string filters (exact match, wildcard, range)
    /// to an IQueryable based on the provided filter dictionary and
    /// optional allowed property list.
    /// </summary>
    public static class QueryableExtensions
    {
        private static readonly Type[] NumericTypes = { typeof(int), typeof(double), typeof(decimal) };

        public static IQueryable<T> ApplyDynamicFilters<T>(
            this IQueryable<T> query,
            IDictionary<string, string> filters,
            IEnumerable<string>? allowed = null)
        {
            if (filters is null || filters.Count == 0)
                return query;

            var map = new Dictionary<string, (string Path, Type Type)>(StringComparer.OrdinalIgnoreCase);
            BuildMap(typeof(T), null, map);

            var allowedSet = allowed?.ToHashSet(StringComparer.OrdinalIgnoreCase);
            foreach (var (rawKey, rawVal) in filters)
            {
                var val = rawVal?.Trim();
                if (string.IsNullOrEmpty(val)) continue;

                var (leaf, op) = rawKey.StartsWith("_min", StringComparison.OrdinalIgnoreCase)
                    ? (rawKey[4..], ">=")
                    : rawKey.StartsWith("_max", StringComparison.OrdinalIgnoreCase)
                        ? (rawKey[4..], "<=")
                        : (rawKey, "==");

                if (allowedSet?.Contains(leaf) == false) continue;
                if (!map.TryGetValue(leaf, out var info)) continue;

                var (path, type) = info;
                if (type == typeof(string))
                {
                    var clean = val.Trim('*');
                    var method = val.StartsWith("*")
                        ? val.EndsWith("*") ? "Contains" : "EndsWith"
                        : val.EndsWith("*") ? "StartsWith" : null;

                    query = method is null
                        ? query.Where($"{path} == @0", clean)
                        : query.Where($"{path}.{method}(@0)", clean);
                }
                else if (NumericTypes.Contains(type) || type == typeof(DateTime))
                {
                    var typed = Convert.ChangeType(val, type, CultureInfo.InvariantCulture)!;
                    query = query.Where($"{path} {op} @0", typed);
                }
            }

            return query;
        }

        private static void BuildMap(
            Type current,
            string? prefix,
            Dictionary<string, (string, Type)> map)
        {
            foreach (var p in current.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var path = prefix is null ? p.Name : $"{prefix}.{p.Name}";
                var t = p.PropertyType;

                if (t == typeof(string)
                 || NumericTypes.Contains(t)
                 || t == typeof(DateTime))
                {
                    map[p.Name] = (path, t);
                }
                else if (t.IsClass)
                {
                    BuildMap(t, path, map);
                }
            }
        }
    

        public static IEnumerable<string> GetPropertyNames(this Type type)
        {
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var t = prop.PropertyType;
                if (t == typeof(string)
                    || t == typeof(bool)
                    || t == typeof(DateTime)
                    || NumericTypes.Contains(t))
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
