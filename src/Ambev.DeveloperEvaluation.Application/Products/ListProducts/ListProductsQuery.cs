using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.Common.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    /// <summary>
    /// Represents a query to retrieve a paginated list of products.
    /// </summary>
    public class ListProductsQuery : IRequest<PaginatedList<ProductResult>>
    {
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
        public string? Order { get; set; }
    }
}
