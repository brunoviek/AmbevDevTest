using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
    /// <summary>
    /// Validator for <see cref="ListUsersQuery"/>.
    /// Ensures pagination parameters are valid and the optional Order string
    /// follows "field1 asc, field2 desc" pattern.
    /// </summary>
    public class ListUsersQueryValidator : AbstractValidator<ListUsersQuery>
    {
        public ListUsersQueryValidator()
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
