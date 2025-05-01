using Ambev.DeveloperEvaluation.Application.Users.Shared.Models;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Requets;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared
{
    /// <summary>
    /// Profile for mapping User results and responses features
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// Initializes the mapping User results and responses features
        /// </summary>
        public UserProfile() 
        {
            CreateMap<UserResult, UserResponse>();
            CreateMap<UserNameResult, UserNameResponse>();
            CreateMap<UserAddressResult, UserAddressResponse>();
            CreateMap<UserGeolocationResult, UserGeolocationResponse>()
                    .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Latitude))
                    .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Longitude));
            CreateMap<UserNameRequest, UserNameModel>();
            CreateMap<UserGeolocationRequest, UserGeolocationModel>()
                    .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Lat))
                    .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Long));
            CreateMap<UserAddressRequest, UserAddressModel>();
        }
    }
}
