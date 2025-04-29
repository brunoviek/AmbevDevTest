using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
    /// <summary>
    /// Validator for <see cref="ListUsersCommand"/>.
    /// Ensures pagination parameters are valid and the optional Order string
    /// follows "field1 asc, field2 desc" pattern.
    /// </summary>
    public class ListUsersCommandValidator : AbstractValidator<ListUsersCommand>
    {
        public ListUsersCommandValidator()
        {
            // Page must be at least 1
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page number must be at least 1.");

            // Size must be between 1 and 20
            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Page size must be at least 1.")
                .LessThanOrEqualTo(20)
                .WithMessage("Page size must not exceed 20.");

            // Order, if provided, must match comma-separated "Field asc|desc" segments
            RuleFor(x => x.Order)
                .Matches(@"^[a-zA-Z0-9_]+\s+(asc|desc)(\s*,\s*[a-zA-Z0-9_]+\s+(asc|desc))*$")
                .WithMessage("Order must be in the format \"field1 asc, field2 desc\".")
                .When(x => !string.IsNullOrWhiteSpace(x.Order));
        }
    }
}
