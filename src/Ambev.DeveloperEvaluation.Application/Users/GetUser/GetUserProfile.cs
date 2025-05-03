using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Profile for mapping between Products entity and GetUserResponse
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<User, UserResult>();
        CreateMap<Name, UserNameResult>();
        CreateMap<Geolocation, UserGeolocationResult>();
        CreateMap<Address, UserAddressResult>();
    }
}
