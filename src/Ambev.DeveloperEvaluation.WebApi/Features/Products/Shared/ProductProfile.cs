using Ambev.DeveloperEvaluation.Application.Products.Shared.Models;
using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared.Requets;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared
{
    /// <summary>
    /// Profile for mapping Products results and responses features
    /// </summary>
    public class ProductProfile : Profile
    {
        /// <summary>
        /// Initializes the mapping Products results and responses features
        /// </summary>
        public ProductProfile()
        {
            CreateMap<ProductResult, ProductResponse>();
            CreateMap<RatingResult, RatingResponse>();
            CreateMap<RatingRequest, RatingModel>(); 
        }
    }
}
