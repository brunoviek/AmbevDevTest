namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Requets
{
    /// <summary>
    /// Request for Product Name
    /// </summary>
    public class UserNameRequest
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
