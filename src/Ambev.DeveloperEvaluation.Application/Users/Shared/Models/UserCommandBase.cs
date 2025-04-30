using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.Shared.Models
{
    /// <summary>
    /// Base class for user-related commands containing common user data fields.
    /// </summary>
    public abstract class UserCommandBase
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number for the user.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address for the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status of the user.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets the user's full name information.
        /// </summary>
        public UserNameModel? Name { get; set; }

        /// <summary>
        /// Gets or sets the user's address information.
        /// </summary>
        public UserAddressModel? Address { get; set; }
    }
}
