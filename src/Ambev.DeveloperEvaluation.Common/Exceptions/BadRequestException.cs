using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Common.Exceptions
{
    /// <summary>
    /// Represents an error caused by an invalid client request,
    /// which should be translated into an HTTP 400 (Bad Request) response.
    /// </summary>
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BadRequestException"/>
        /// with a specified error message describing the reason for the failure.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
