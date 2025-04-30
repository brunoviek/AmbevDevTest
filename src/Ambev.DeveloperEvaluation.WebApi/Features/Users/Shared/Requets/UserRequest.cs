using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Responses;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Requets
{
    /// <summary>
    /// Abstract base request that represents common user input fields used in user operations.
    /// </summary>
    public abstract class UserRequest
    {
        /// <summary>
        /// Gets or sets the username. Must be unique and contain only valid characters.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password. Must meet security requirements.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number in format (XX) XXXXX-XXXX.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address. Must be a valid email format.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the initial status of the user account.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the role assigned to the user.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets the user's full name details.
        /// </summary>
        public UserNameRequest? Name { get; set; }

        /// <summary>
        /// Gets or sets the user's address details.
        /// </summary>
        public UserAddressRequest? Address { get; set; }
    }
}
