using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    /// <summary>
    /// AutoMapper profile for mapping data between UpdateProductCommand and domain entities.
    /// </summary>
    public class UpdateProductProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductProfile"/> class.
        /// Configures mappings from UpdateProductCommand to Products and nested value objects.
        /// </summary>
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductCommand, Product>();
        }
    }
}
