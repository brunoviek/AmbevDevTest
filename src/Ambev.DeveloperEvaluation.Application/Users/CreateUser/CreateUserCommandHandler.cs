using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.Common.Exceptions;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;
using Rebus.Bus;
using Ambev.DeveloperEvaluation.Application.Users.Events;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IBus _bus;

    /// <summary>
    /// Initializes a new instance of CreateUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher, IBus bus)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _bus = bus;
    }

    /// <summary>
    /// Handles the CreateUserCommand request
    /// </summary>
    /// <param name="command">The CreateUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    public async Task<UserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (existingUser != null)
            throw new BadRequestException($"Products with email {command.Email} already exists");

        var user = _mapper.Map<User>(command);
        user.Password = _passwordHasher.HashPassword(command.Password);

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);

        await _bus.Publish(new UserRegisteredEvent(createdUser));

        var result = _mapper.Map<UserResult>(createdUser);
        return result;
    }
}
