using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Models;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between Products entity and CreateUserResponse
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<UserNameModel, Name>();
        CreateMap<UserGeolocationModel, Geolocation>();
        CreateMap<UserAddressModel, Address>();
        CreateMap<User, UserResult>();
        CreateMap<Address, UserAddressResult>();
        CreateMap<Geolocation, UserGeolocationResult>();
        CreateMap<Name, UserNameResult>();
    }
}
