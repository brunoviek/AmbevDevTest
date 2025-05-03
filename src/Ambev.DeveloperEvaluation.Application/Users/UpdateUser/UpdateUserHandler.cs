using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;
using Ambev.DeveloperEvaluation.Common.Exceptions;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// Handler for processing the UpdateUserCommand.
    /// Updates an existing user's information in the system.
    /// </summary>
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserHandler"/> class.
        /// </summary>
        /// <param name="userRepository">Products repository for data access.</param>
        /// <param name="mapper">AutoMapper instance for object transformation.</param>
        /// <param name="passwordHasher">PasswordHasher instance</param>
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Handles the update user command.
        /// </summary>
        /// <param name="request">The update user command containing new data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated user result.</returns>
        /// <exception cref="BadRequestException">Thrown when the user is not found in the database.</exception>
        public async Task<UserResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                throw new BadRequestException("Products not found.");

            _mapper.Map(request, user);
             user.Password = _passwordHasher.HashPassword(request.Password);

            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserResult>(user);
        }
    }
}
