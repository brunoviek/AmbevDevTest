using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Users.Results;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

/// <summary>
/// Handler for processing DeleteUserCommand requests
/// </summary>
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, GetUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of DeleteUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    public DeleteUserHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the DeleteUserCommand request
    /// </summary>
    /// <param name="request">The DeleteUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<GetUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {request.Id} not found");

        var success = await _userRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Error on deleting user with ID {request.Id}");

        return _mapper.Map<GetUserResult>(user);
    }
}
