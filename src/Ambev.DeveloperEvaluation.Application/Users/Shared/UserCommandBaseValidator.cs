using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Models;

namespace Ambev.DeveloperEvaluation.Application.Users.Shared
{
    /// <summary>
    /// Base validator for user commands.
    /// </summary>
    public abstract class UserCommandBaseValidator<T> : AbstractValidator<T>
        where T : UserCommandBase
    {
        protected UserCommandBaseValidator()
        {
            RuleFor(user => user.Email).SetValidator(new EmailValidator());
            RuleFor(user => user.Username).NotEmpty().Length(3, 50);
            RuleFor(user => user.Password).SetValidator(new PasswordValidator());
            RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
            RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
            RuleFor(user => user.Role).NotEqual(UserRole.None);
            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name!.Firstname).NotEmpty().MaximumLength(30);
                RuleFor(x => x.Name!.Lastname).NotEmpty().MaximumLength(30);
            });
            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address!.Street).NotEmpty();
                RuleFor(x => x.Address!.City).NotEmpty();
                RuleFor(x => x.Address!.Zipcode).NotEmpty();
                RuleFor(x => x.Address!.Geolocation!.Latitude).NotEmpty();
                RuleFor(x => x.Address!.Geolocation!.Longitude).NotEmpty();
            });
        }
    }
}
