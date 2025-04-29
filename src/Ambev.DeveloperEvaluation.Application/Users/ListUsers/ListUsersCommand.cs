using Ambev.DeveloperEvaluation.Application.Users.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    /// <summary>
    /// Command for listing users with pagination and optional ordering.
    /// </summary>
    public class ListUsersCommand : IRequest<IEnumerable<GetUserResult>>
    {
        /// <summary>
        /// Gets or sets the page number for pagination.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int Size { get; set; } = 10;

        /// <summary>
        /// Gets or sets the ordering criteria (e.g., "username asc, email desc").
        /// </summary>
        public string? Order { get; set; }
    }
}
