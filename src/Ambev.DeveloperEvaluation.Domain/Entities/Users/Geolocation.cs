namespace Ambev.DeveloperEvaluation.Domain.Entities.Users;

/// <summary>
/// Represents the geographic location (latitude and longitude) of an address.
/// </summary>
public class Geolocation
{
    /// <summary>
    /// Gets or sets the latitude of the address location.
    /// </summary>
    public string Latitude { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the longitude of the address location.
    /// </summary>
    public string Longitude { get; set; } = string.Empty;
}
