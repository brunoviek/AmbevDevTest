using FluentValidation.TestHelper;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth.AuthenticateUser
{
    /// <summary>
    /// Unit tests for <see cref="AuthenticateUserValidator"/>, ensuring correct
    /// validation rules for Email and Password fields.
    /// </summary>
    public class AuthenticateUserValidatorTests
    {
        private readonly AuthenticateUserValidator _validator;

        public AuthenticateUserValidatorTests()
        {
            _validator = new AuthenticateUserValidator();
        }

        /// <summary>
        /// Validates that a command with valid Email and Password passes without errors.
        /// </summary>
        [Fact(DisplayName = "Valid command should not produce validation errors")]
        public void Validate_ValidCommand_NoValidationErrors()
        {
            var command = new AuthenticateUserCommand
            {
                Email = "user@qlqrcoisa.com",
                Password = "Senha1"
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Validates that empty or whitespace Email triggers a NotEmpty error.
        /// </summary>
        [Fact(DisplayName = "Empty or whitespace email should produce NotEmpty validation error")]
        public void Validate_EmptyOrWhitespaceEmail_HasNotEmptyError()
        {
            foreach (var email in new[] { string.Empty, "   " })
            {
                var command = new AuthenticateUserCommand { Email = email, Password = "Secure1" };
                var result = _validator.TestValidate(command);
                result.ShouldHaveValidationErrorFor(c => c.Email);
            }
        }

        /// <summary>
        /// Validates that invalid Email format triggers an EmailAddress error.
        /// </summary>
        [Fact(DisplayName = "Invalid email format should produce EmailAddress validation error")]
        public void Validate_InvalidEmail_HasEmailAddressError()
        {
            foreach (var email in new[] { "user@", "aszasfasf" })
            {
                var command = new AuthenticateUserCommand { Email = email, Password = "Secure1" };
                var result = _validator.TestValidate(command);
                result.ShouldHaveValidationErrorFor(c => c.Email);
            }
        }

        /// <summary>
        /// Validates that empty or whitespace Password triggers a NotEmpty error.
        /// </summary>
        [Fact(DisplayName = "Empty or whitespace password should produce NotEmpty validation error")]
        public void Validate_EmptyOrWhitespacePassword_HasNotEmptyError()
        {
            foreach (var pwd in new[] { null, string.Empty, "   " })
            {
                var command = new AuthenticateUserCommand { Email = "user@asdasda.com", Password = pwd! };
                var result = _validator.TestValidate(command);
                result.ShouldHaveValidationErrorFor(c => c.Password);
            }
        }

        /// <summary>
        /// Validates that short Password triggers a MinimumLength error.
        /// </summary>
        [Fact(DisplayName = "Password too short should produce MinimumLength validation error")]
        public void Validate_ShortPassword_HasMinimumLengthError()
        {
            foreach (var pwd in new[] { "12345", "abcde" })
            {
                var command = new AuthenticateUserCommand { Email = "user@asfdavg.com", Password = pwd };
                var result = _validator.TestValidate(command);
                result.ShouldHaveValidationErrorFor(c => c.Password);
            }
        }
    }
}
