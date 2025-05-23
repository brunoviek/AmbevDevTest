using Ambev.DeveloperEvaluation.Domain.Enums;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Responses;

/// <summary>
/// API response model for GetUser operation
/// </summary>
public class UserResponse
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
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserRole Role { get; set; }

    /// <summary>
    /// The current status of the user
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the user's full name details.
    /// </summary>
    public UserNameResponse? Name { get; set; }

    /// <summary>
    /// Gets or sets the user's address details.
    /// </summary>
    public UserAddressResponse? Address { get; set; }
}
