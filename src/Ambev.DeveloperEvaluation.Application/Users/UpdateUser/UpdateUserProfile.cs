using Ambev.DeveloperEvaluation.Application.Users.Shared.Models;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// AutoMapper profile for mapping data between UpdateUserCommand and domain entities.
    /// </summary>
    public class UpdateUserProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserProfile"/> class.
        /// Configures mappings from UpdateUserCommand to Products and nested value objects.
        /// </summary>
        public UpdateUserProfile()
        {
            CreateMap<UpdateUserCommand, User>();
        }
    }
}
