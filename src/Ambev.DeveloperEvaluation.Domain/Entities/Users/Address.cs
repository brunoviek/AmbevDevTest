namespace Ambev.DeveloperEvaluation.Domain.Entities.Users;

/// <summary>
/// Represents the address of a user, including street, number, city, zip code, and geolocation.
/// </summary>
public class Address
{
    /// <summary>
    /// Gets or sets the street name of the user's address.
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of the user's address.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the city of the user's address.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the zipcode of the user's address.
    /// </summary>
    public string Zipcode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geolocation details of the user's address.
    /// </summary>
    public Geolocation Geolocation { get; set; } = new Geolocation();
}
