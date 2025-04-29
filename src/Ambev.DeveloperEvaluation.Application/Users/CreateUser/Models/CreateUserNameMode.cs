using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser.Models
{
    /// <summary>
    /// Represents the user's full name information for user creation.
    /// </summary>
    public class CreateUserNameModel
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string Firstname { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string Lastname { get; set; } = string.Empty;
    }
}
