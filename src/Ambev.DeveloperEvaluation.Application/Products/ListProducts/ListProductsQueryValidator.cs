using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    /// <summary>
    /// Validator for <see cref="ListProductsQuery"/>, ensuring pagination parameters and ordering syntax are valid.
    /// </summary>
    public class ListProductsQueryValidator : AbstractValidator<ListProductsQuery>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ListProductsQueryValidator"/>, applying validation rules.
        /// </summary>
        public ListProductsQueryValidator()
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
                .Matches(@"^[a-zA-Z0-9_]+\s+(asc|desc)(\s*,\s*[a-zA-Z0-9_]+\s+(asc|desc))*$")
                .WithMessage("Order must be in the format \"field1 asc, field2 desc\".")
                .When(x => !string.IsNullOrWhiteSpace(x.Order));
        }
    }
}