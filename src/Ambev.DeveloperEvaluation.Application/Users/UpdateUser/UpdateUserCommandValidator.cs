using Ambev.DeveloperEvaluation.Application.Users.Shared;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{ 
    /// <summary>
    /// Validator for UpdateUserCommand.
    /// </summary>
    public class UpdateUserCommandValidator : UserCommandBaseValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
