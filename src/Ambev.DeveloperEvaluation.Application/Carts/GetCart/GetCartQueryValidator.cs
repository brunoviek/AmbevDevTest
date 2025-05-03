using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    /// <summary>
    /// Validator for <see cref="GetCartQuery"/>, ensuring the cart ID is valid.
    /// </summary>
    public class GetCartQueryValidator : AbstractValidator<GetCartQuery>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GetCartQueryValidator"/>.
        /// </summary>
        public GetCartQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
