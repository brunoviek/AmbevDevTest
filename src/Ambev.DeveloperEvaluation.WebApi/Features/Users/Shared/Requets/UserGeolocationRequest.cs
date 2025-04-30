namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Requets
{
    /// <summary>
    /// Represents the geolocation (latitude and longitude) of the user's address.
    /// </summary>
    public class UserGeolocationRequest
    {
        /// <summary>
        /// Gets or sets the latitude coordinate.
        /// </summary>
        public string Lat { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the longitude coordinate.
        /// </summary>
        public string Long { get; set; } = string.Empty;
    }
}
