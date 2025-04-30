using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    /// <summary>
    /// Represents a query to retrieve a <see cref="ProductResult"/> by its identifier.
    /// </summary>
    public class GetProductQuery : IRequest<ProductResult>
    {
        public GetProductQuery(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the product to retrieve.
        /// </summary>
        public int Id { get; set; }
    }
}
