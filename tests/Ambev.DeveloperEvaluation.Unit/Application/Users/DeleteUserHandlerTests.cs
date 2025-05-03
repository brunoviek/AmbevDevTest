using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    /// <summary>
    /// Contains unit tests for the <see cref="DeleteUserCommandHandler"/> class.
    /// </summary>
    public class DeleteUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly DeleteUserCommandHandler _handler;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserHandlerTests"/> class.
        /// Sets up the test dependencies and mocks.
        /// </summary>
        public DeleteUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new DeleteUserCommandHandler(_userRepository, _mapper);
        }

        /// <summary>
        /// Tests that a valid delete request is handled successfully.
        /// Uses <see cref="CreateUserHandlerTestData"/> to generate all user fields,
        /// then ensures:
        /// 1. GetByIdAsync is called once.
        /// 2. DeleteAsync is called once.
        /// 3. The entity is mapped to <see cref="UserResult"/> and returned.
        /// </summary>
        [Fact(DisplayName = "Given valid ID When handling Then returns mapped GetUserResult")]
        public async Task Handle_ValidRequest_ReturnsMappedGetUserResult_UsingTestData()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new DeleteUserCommand(userId);

            // Prepare a fake user entity
            var userEntity = UserTestData.GenerateValidUser();
            userEntity.Id = userId;

            _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<User?>(userEntity));

            _userRepository
                .DeleteAsync(userId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(true));

            _mapper
                .Map<UserResult>(Arg.Any<User>())
                .Returns(new UserResult());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert: verify only that the dependencies were invoked
            await _userRepository.Received(1).GetByIdAsync(userId, CancellationToken.None);
            await _userRepository.Received(1).DeleteAsync(userId, CancellationToken.None);
            _mapper.Received(1).Map<UserResult>(userEntity);
        }

        /// <summary>
        /// Tests that deleting a non-existent user throws a <see cref="KeyNotFoundException"/>.
        /// </summary>
        [Fact(DisplayName = "Given non-existent ID When handling Then throws KeyNotFoundException")]
        public async Task Handle_UserNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new DeleteUserCommand(userId);
            _userRepository
                .DeleteAsync(userId, Arg.Any<CancellationToken>())
                .Returns(false);

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Products with ID {userId} not found");
        }

        /// <summary>
        /// Tests that a deletion request with an empty Guid throws a <see cref="ValidationException"/>.
        /// </summary>
        [Fact(DisplayName = "Given empty ID When handling Then throws ValidationException")]
        public void Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var validator = new DeleteUserValidator();
            var command = new DeleteUserCommand(Guid.Empty);

            // Act & Assert
            validator
              .Invoking(v => v.ValidateAndThrow(command))
              .Should()
              .Throw<ValidationException>();
        }
    }
}
