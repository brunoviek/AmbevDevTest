using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.Common.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProductsByCategory
{
    /// <summary>
    /// Represents a query to retrieve a paginated list of products.
    /// </summary>
    public class ListProductsByCategoryQuery : IRequest<PaginatedList<ProductResult>>
    {
        /// <summary>
        /// Category name to filter products.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the page number for pagination.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int Size { get; set; } = 10;

        /// <summary>
        /// Gets or sets the ordering criteria (e.g., "title asc, price desc").
        /// </summary>
        public string? Order
        {
            get => _order;
            set => _order = value?.Trim();
        }

        private string? _order;

        /// <summary>
        /// Gets or sets Filters to apply
        /// </summary>
        public IDictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();
    }
}
