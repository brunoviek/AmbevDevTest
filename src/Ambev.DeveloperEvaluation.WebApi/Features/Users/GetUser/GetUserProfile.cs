using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.Shared;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Responses;
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
        CreateMap<Guid, GetUserQuery>()
            .ConstructUsing(id => new GetUserQuery(id));

    }
}
