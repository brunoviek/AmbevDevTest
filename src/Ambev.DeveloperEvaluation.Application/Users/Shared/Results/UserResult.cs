using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.Shared.Results;

/// <summary>
/// Response model for GetUser operation
/// </summary>
public class UserResult
{
    /// <summary>
    /// The unique identifier of the user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user's full name
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The user's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;   

    /// <summary>
    /// The user's role in the system
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// The current status of the user
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the name details of the user (firstname and lastname).
    /// </summary>
    public UserNameResult? Name { get; set; }

    /// <summary>
    /// Gets or sets the address details of the user, including geolocation.
    /// </summary>
    public UserAddressResult? Address { get; set; }
}
