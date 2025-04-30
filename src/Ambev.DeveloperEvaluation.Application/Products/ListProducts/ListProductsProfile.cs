using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities.Product;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    /// <summary>
    /// AutoMapper profile for mapping <see cref="Product"/> entities to <see cref="ProductResult"/> DTOs
    /// when listing products.
    /// </summary>
    public class ListProductsProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListProductsProfile"/> class.
        /// </summary>
        public ListProductsProfile()
        {

        }
    }
}