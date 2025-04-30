using Ambev.DeveloperEvaluation.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Common
{
    /// <summary>
    /// Provides a mechanism to validate sorting expressions against a whitelist of allowed fields,
    /// ensuring consistent ordering rules for different entity types without duplicating logic.
    /// </summary>
    public static class OrderValidator
    {
        // Fields allowed for user sorting
        private static readonly ISet<string> AllowedUserFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Username",
            "Email",
            "Phone",
            "Status",
            "Role",
            "CreatedAt",
            "FirstName",
            "LastName",
            "City",
            "Street",
            "Zipcode"
        };

        // Fields allowed for product sorting
        private static readonly ISet<string> AllowedProductFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "id",
            "title",
            "price",
            "category",
            "rate",
            "count"
        };

        /// <summary>
        /// Validates a user-specific sorting expression against predefined allowed fields.
        /// </summary>
        /// <param name="orderBy">
        /// The sorting expression supplied by the client, e.g., "Email desc,CreatedAt asc".
        /// </param>
        /// <returns>
        /// The original <paramref name="orderBy"/> string if validation succeeds.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the expression contains field names not present in the whitelist.
        /// </exception>
        public static string ValidateUserOrderFields(string orderBy)
            => Validate(orderBy, AllowedUserFields);

        /// <summary>
        /// Validates a product-specific sorting expression against predefined allowed fields.
        /// </summary>
        /// <param name="orderBy">
        /// The sorting expression supplied by the client, e.g., "Email desc,CreatedAt asc".
        /// </param>
        /// <returns>
        /// The original <paramref name="orderBy"/> string if validation succeeds.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the expression contains field names not present in the whitelist.
        /// </exception>
        public static string ValidateProductOrderFields(string orderBy)
            => Validate(orderBy, AllowedProductFields);

        // validate method
        private static string Validate(string orderBy, IEnumerable<string> allowedFields)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return orderBy;
            }

            var segments = orderBy.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var segment in segments)
            {
                var fieldName = segment.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
                if (!allowedFields.Contains(fieldName))
                {
                    throw new BadRequestException($"Invalid ordering field: {fieldName}");
                }
            }

            return orderBy;
        }
    }
}
