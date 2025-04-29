using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities.User;
using Ambev.DeveloperEvaluation.Application.Users.Results;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<User, GetUserResult>();
        CreateMap<Name, GetUserNameResult>();
        CreateMap<Geolocation, GetUserGeolocationResult>();
        CreateMap<Address, GetUserAddressResult>();
    }
}
