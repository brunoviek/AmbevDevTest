using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities.Product;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    /// <summary>
    /// AutoMapper profile for mapping <see cref="Product"/> to <see cref="ProductResult"/>.
    /// </summary>
    internal class GetProductProfile : Profile
    {
        /// <summary>
        /// Initializes the mapping configuration.
        /// </summary>
        public GetProductProfile()
        {
        }
    }
}
