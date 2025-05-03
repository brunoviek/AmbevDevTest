using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());

        RuleFor(user => user.Username)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");
        
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        
        RuleFor(user => user.Phone)
            .Matches(@"^\+[1-9]\d{10,14}$")
            .WithMessage("Phone number must start with '+' followed by 11-15 digits.");
        
        RuleFor(user => user.Status)
            .NotEqual(UserStatus.Unknown)
            .WithMessage("Users status cannot be Unknown.");
        
        RuleFor(user => user.Role)
            .NotEqual(UserRole.None)
            .WithMessage("Users role cannot be None.");

        When(user => user.Name != null &&
                          (!string.IsNullOrWhiteSpace(user.Name.Firstname) || !string.IsNullOrWhiteSpace(user.Name.Lastname)), () =>
                          {
                              RuleFor(user => user.Name!.Firstname)
                             .NotEmpty().WithMessage("Firstname is required if Name is informed.")
                             .MinimumLength(3).WithMessage("Firstname must be at least 3 characters long.");

                              RuleFor(user => user.Name!.Lastname)
                             .NotEmpty().WithMessage("Lastname is required if Name is informed.")
                             .MinimumLength(3).WithMessage("Lastname must be at least 3 characters long.");
                          });

        When(user => user.Address != null &&
                     (!string.IsNullOrWhiteSpace(user.Address.Street) ||
                      !string.IsNullOrWhiteSpace(user.Address.City) ||
                      !string.IsNullOrWhiteSpace(user.Address.Zipcode) ||
                      (user.Address.Number != 0) ||
                      (user.Address.Geolocation != null &&
                       (!string.IsNullOrWhiteSpace(user.Address.Geolocation.Latitude) ||
                        !string.IsNullOrWhiteSpace(user.Address.Geolocation.Longitude)))), () =>
                        {
                            RuleFor(user => user.Address!.Street)
                                .NotEmpty().WithMessage("Street is required if Address is informed.")
                                .MinimumLength(3).WithMessage("Street must be at least 3 characters long.");

                            RuleFor(user => user.Address!.City)
                                .NotEmpty().WithMessage("City is required if Address is informed.")
                                .MinimumLength(3).WithMessage("City must be at least 3 characters long.");

                            RuleFor(user => user.Address!.Zipcode)
                                .NotEmpty().WithMessage("Zipcode is required if Address is informed.")
                                .MinimumLength(3).WithMessage("Zipcode must be at least 3 characters long.");

                            RuleFor(user => user.Address!.Number)
                                .GreaterThan(0).WithMessage("Number must be greater than zero if Address is informed.");

                            RuleFor(user => user.Address!.Geolocation.Latitude)
                                .NotEmpty().WithMessage("Latitude is required if Address is informed.")
                                .Must(BeValidLatitude).WithMessage("Latitude must be a valid decimal number between -90 and 90.");

                            RuleFor(user => user.Address!.Geolocation.Longitude)
                                .NotEmpty().WithMessage("Longitude is required if Address is informed.")
                                .Must(BeValidLongitude).WithMessage("Longitude must be a valid decimal number between -180 and 180.");
                        });
    }

    private bool BeValidLatitude(string latitude)
    {
        if (decimal.TryParse(latitude, out var value))
        {
            return value >= -90 && value <= 90;
        }
        return false;
    }

    private bool BeValidLongitude(string longitude)
    {
        if (decimal.TryParse(longitude, out var value))
        {
            return value >= -180 && value <= 180;
        }
        return false;
    }
}
