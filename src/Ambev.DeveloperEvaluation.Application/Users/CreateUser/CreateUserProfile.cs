using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities.User;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser.Models;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<CreateUserNameModel, Name>();
        CreateMap<CreateUserGeolocationModel, Geolocation>();
        CreateMap<CreateUserAddressModel, Address>();
        CreateMap<User, CreateUserResult>();
    }
}
