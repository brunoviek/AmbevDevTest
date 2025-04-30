using Ambev.DeveloperEvaluation.Application.Users.Shared.Models;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;
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
