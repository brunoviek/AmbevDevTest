using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;
using Ambev.DeveloperEvaluation.Common.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    /// <summary>
    /// Command for listing users with pagination and optional ordering.
    /// </summary>
    public class ListUsersQuery : IRequest<PaginatedList<UserResult>>
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
        public string? Order
        {
            get => _order;
            set => _order = value?.Trim();
        }

        private string? _order;

        /// <summary>
        /// Gets or sets Filters to apply
        /// </summary>
        public IDictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();
    }
}
