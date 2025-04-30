using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Models;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// Command for updating an existing user.
    /// </summary>
    public class UpdateUserCommand : UserCommandBase, IRequest<UserResult>
    {
        public Guid Id { get; set; }
    }
}
