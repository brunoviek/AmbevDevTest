using Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Responses;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Requets
{
    /// <summary>
    /// Represents the user's address details in the response, including geolocation.
    /// </summary>
    public class UserAddressRequest
    {
        /// <summary>
        /// Gets or sets the street of the address.
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the house or building number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the postal or zip code.
        /// </summary>
        public string Zipcode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the geolocation details of the address.
        /// </summary>
        public UserGeolocationRequest? Geolocation { get; set; }
    }
}
