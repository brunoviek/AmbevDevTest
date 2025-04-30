using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    /// <summary>
    /// Command to delete an existing product by its identifier.
    /// </summary>
    public class DeleteProductCommand : IRequest<ProductResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        public DeleteProductCommand(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the product to delete.
        /// </summary>
        public int Id { get; set; }
    }
}
