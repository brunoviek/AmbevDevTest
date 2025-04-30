using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
    /// <summary>
    /// Profile for mapping ListUsers feature requests to commands
    /// </summary>
    public class ListUsersProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for ListUsers feature
        /// </summary>
        public ListUsersProfile() {
            CreateMap<ListUsersRequest, ListUsersQuery>();
        }
    }
}
