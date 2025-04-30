namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Responses
{
    /// <summary>
    /// Represents the geolocation (latitude and longitude) of the user's address.
    /// </summary>
    public class UserGeolocationResponse
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
