using Ambev.DeveloperEvaluation.Application.Users.Shared;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser
{
    /// <summary>
    /// Validator for CreateUserCommand that defines validation rules for user creation command.
    /// </summary>
    public class CreateUserCommandValidator : UserCommandBaseValidator<CreateUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateUserCommandValidator with defined validation rules.
        /// </summary>
        public CreateUserCommandValidator()
        {
        }
    }
}