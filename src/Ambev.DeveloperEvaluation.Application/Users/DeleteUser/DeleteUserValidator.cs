using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

/// <summary>
/// Validator for DeleteUserCommand
/// </summary>
public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteUserCommand
    /// </summary>
    public DeleteUserValidator(ICartRepository cartRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Products ID is required");

        RuleFor(x => x.Id)
            .MustAsync(NotInAnyCart)
            .WithMessage("Cannot delete user because they have existing carts.")
            .WithErrorCode("UserInUse");

        Task<bool> NotInAnyCart(Guid userId, CancellationToken ct)
        {
            var exists = cartRepository.QueryAll().Any(c => c.UserId == userId);
            return Task.FromResult(!exists);
        }
    }
}
