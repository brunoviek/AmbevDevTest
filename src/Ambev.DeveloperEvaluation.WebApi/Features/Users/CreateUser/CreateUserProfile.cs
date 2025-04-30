using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Requets;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser.Models;
using Ambev.DeveloperEvaluation.Application.Users.Shared;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
    }
}
