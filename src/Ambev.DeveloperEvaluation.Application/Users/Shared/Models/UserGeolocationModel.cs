using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.Shared.Models
{
    /// <summary>
    /// Represents the geolocation (latitude and longitude) for user address creation.
    /// </summary>
    public class UserGeolocationModel
    {
        /// <summary>
        /// Gets or sets the latitude coordinate.
        /// </summary>
        public string Latitude { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the longitude coordinate.
        /// </summary>
        public string Longitude { get; set; } = string.Empty;
    }
}
