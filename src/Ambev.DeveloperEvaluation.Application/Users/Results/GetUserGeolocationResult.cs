namespace Ambev.DeveloperEvaluation.Application.Users.Results
{
    /// <summary>
    /// Represents the geolocation details (latitude and longitude) of the user's address.
    /// </summary>
    public class GetUserGeolocationResult
    {
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
    }
}
