using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.Shared.Models
{
    /// <summary>
    /// Represents the user's address information for user creation, including geolocation.
    /// </summary>
    public class UserAddressModel
    {
        /// <summary>
        /// Gets or sets the street of the user's address.
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the house or building number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the city where the user lives.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the postal code (zipcode) of the address.
        /// </summary>
        public string Zipcode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the geolocation coordinates (latitude and longitude) of the address.
        /// </summary>
        public UserGeolocationModel? Geolocation { get; set; }
    }
}
