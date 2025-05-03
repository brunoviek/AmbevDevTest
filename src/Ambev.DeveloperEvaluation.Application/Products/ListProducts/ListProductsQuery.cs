using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.Common.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    /// <summary>
    /// Represents a query to retrieve a paginated list of products.
    /// </summary>
    public class ListProductsQuery : ListQueryBase, IRequest<PaginatedList<ProductResult>>
    {
        /// <summary>
        /// Gets or sets Filters to apply
        /// </summary>
        public IDictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();
    }
}
