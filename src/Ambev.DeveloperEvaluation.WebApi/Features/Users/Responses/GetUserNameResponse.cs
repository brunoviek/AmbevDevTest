namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Responses
{
    /// <summary>
    /// Represents the user's name details in the response.
    /// </summary>
    public class GetUserNameResponse
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string Firstname { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string Lastname { get; set; } = string.Empty;
    }
}
