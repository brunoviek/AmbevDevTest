using Ambev.DeveloperEvaluation.Application.Users.Results;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.Responses;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Profile for mapping GetUser feature requests to commands
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser feature
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<Guid, Application.Users.GetUser.GetUserCommand>()
            .ConstructUsing(id => new Application.Users.GetUser.GetUserCommand(id));
        CreateMap<GetUserResult, GetUserResponse>();
        CreateMap<GetUserNameResult, GetUserNameResponse>();
        CreateMap<GetUserAddressResult, GetUserAddressResponse>();
        CreateMap<GetUserGeolocationResult, GetUserGeolocationResponse>()
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Longitude));

    }
}
