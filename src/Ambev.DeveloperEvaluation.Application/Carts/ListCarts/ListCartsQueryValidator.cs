using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    /// <summary>
    /// Validator for <see cref="ListCartsQuery"/>, ensuring pagination parameters and ordering syntax are valid.
    /// </summary>
    public class ListCartsQueryValidator : AbstractValidator<ListCartsQuery>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ListCartsQueryValidator"/>, applying validation rules.
        /// </summary>
        public ListCartsQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page number must be at least 1.");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Page size must be at least 1.")
                .LessThanOrEqualTo(20)
                .WithMessage("Page size must not exceed 20.");

            RuleFor(x => x.Order)
                .Matches(@"^[A-Za-z0-9_]+(?:\s+(?:asc|desc))?(?:\s*,\s*[A-Za-z0-9_]+(?:\s+(?:asc|desc))?)*$")
                .WithMessage("Order must be in the format \"field1 [asc|desc], field2 [asc|desc]\".")
                .When(x => !string.IsNullOrWhiteSpace(x.Order));
        }
    }
}
