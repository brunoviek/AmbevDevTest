namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
    /// <summary>
    /// Represents the query parameters for retrieving a paginated list of users.
    /// </summary>
    public class ListUsersRequest
    {
        /// <summary>
        /// Gets or sets the page number for pagination (optional, default: 1).
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of items per page (optional, default: 10).
        /// </summary>
        public int Size { get; set; } = 10;

        /// <summary>
        /// Gets or sets the ordering criteria (e.g., "username asc, email desc").
        /// </summary>
        public string? Order { get; set; }
    }
}
