using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities.Product;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    /// <summary>
    /// AutoMapper profile for mapping a <see cref="Product"/> entity to <see cref="ProductResult"/> when deleting a product.
    /// </summary>
    public class DeleteProductProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductProfile"/> class.
        /// </summary>
        public DeleteProductProfile()
        {
            
        }
    }
}