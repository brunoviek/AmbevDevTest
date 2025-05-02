using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    /// <summary>
    /// Injects query‐string filter parameters into Swagger for methods
    /// marked with <see cref="DynamicFilterAttribute"/>, reflecting over
    /// the specified DTO’s public leaf properties (string, numeric, DateTime, bool).
    /// Nested complex types are recursed into, but only their leaf children
    /// become filters—parent property names are not used as prefixes.
    /// </summary>
    public class DynamicFilterOperationFilter : IOperationFilter
    {
        private static readonly Type[] NumericTypes =
            { typeof(int), typeof(double), typeof(decimal) };

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attr = context.MethodInfo.GetCustomAttribute<DynamicFilterAttribute>();
            if (attr == null) return;

            var parameters = operation.Parameters ??= new List<OpenApiParameter>();
            AddLeafParams(parameters, attr.EntityType);
        }

        private static void AddLeafParams(IList<OpenApiParameter> parameters, Type type)
        {
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propType = prop.PropertyType;
                if (IsLeaf(propType))
                {
                    AddParameter(parameters, prop.Name, propType);
                    if (IsRangeType(propType))
                    {
                        AddParameter(parameters, $"_min{prop.Name}", propType);
                        AddParameter(parameters, $"_max{prop.Name}", propType);
                    }
                }
                else if (propType.IsClass && propType != typeof(string))
                {
                    AddLeafParams(parameters, propType);
                }
            }
        }

        private static bool IsLeaf(Type t) =>
               t == typeof(string)
            || t == typeof(bool)
            || t == typeof(DateTime)
            || NumericTypes.Contains(t);

        private static bool IsRangeType(Type t) =>
               t == typeof(DateTime)
            || NumericTypes.Contains(t);

        private static void AddParameter(
            IList<OpenApiParameter> parameters,
            string name,
            Type type)
        {
            parameters.Add(new OpenApiParameter
            {
                Name = name,
                In = ParameterLocation.Query,
                Required = false,
                Schema = new OpenApiSchema { Type = MapType(type) }
            });
        }

        private static string MapType(Type t)
        {
            if (t == typeof(string)) return "string";
            if (t == typeof(bool)) return "boolean";
            if (t == typeof(DateTime)) return "string"; 
            if (NumericTypes.Contains(t)) return "number";
            return "string";
        }
    }
}
