﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.Shared.Results
{
    /// <summary>
    /// Represents the address details of the user, including geolocation.
    /// </summary>
    public class UserAddressResult
    {
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; }
        public string City { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public UserGeolocationResult? Geolocation { get; set; }
    }
}
