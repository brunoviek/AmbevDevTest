using Ambev.DeveloperEvaluation.Application.Carts.Shared.Models;
using Ambev.DeveloperEvaluation.Application.Carts.Shared.Results;
using Ambev.DeveloperEvaluation.Application.Products.Shared.Models;
using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.Shared.Requets;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.Shared.Responses;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared.Requets;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.Shared
{
    /// <summary>
    /// Profile for mapping Cart results and responses features
    /// </summary>
    public class CartProfile : Profile
    {
        /// <summary>
        /// Initializes the mapping Cart results and responses features
        /// </summary>
        public CartProfile()
        {
            CreateMap<CartResult, CartResponse>();
            CreateMap<CartProductResult, CartProductResponse>();
            CreateMap<CartProductRequest, CartProductModel>();
        }
    }
}
