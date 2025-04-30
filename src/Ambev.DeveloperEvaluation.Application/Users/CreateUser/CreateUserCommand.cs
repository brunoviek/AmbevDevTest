using Ambev.DeveloperEvaluation.Application.Users.CreateUser.Models;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser
{
    /// <summary>
    /// Command for creating a new user.
    /// </summary>
    public class CreateUserCommand : UserCommandBase, IRequest<UserResult>
    {
    }
}
