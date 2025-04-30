using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities.User;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth
{
    /// <summary>
    /// Unit tests for <see cref="AuthenticateUserCommandHandler"/>, covering
    /// successful authentication and failure scenarios.
    /// </summary>
    public class AuthenticateUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly AuthenticateUserCommandHandler _handler;

        public AuthenticateUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
            _handler = new AuthenticateUserCommandHandler(
                _userRepository,
                _passwordHasher,
                _jwtTokenGenerator);
        }

        /// <summary>
        /// Tests that valid credentials return a populated result.
        /// </summary>
        [Fact]
        public async Task Handle_ValidCredentials_ReturnsResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Email = "test@example.com",
                Password = "hashedPassword",
                Phone = "12345",
                Role = UserRole.Customer,
                Status = UserStatus.Active
            };
            var command = new AuthenticateUserCommand
            {
                Email = user.Email,
                Password = "rawPassword"
            };

            _userRepository
                .GetByEmailAsync(user.Email, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<User?>(user));
            _passwordHasher
                .VerifyPassword(command.Password, user.Password)
                .Returns(true);
            _jwtTokenGenerator
                .GenerateToken(user)
                .Returns("fake-jwt-token");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Token.Should().Be("fake-jwt-token");
            result.Email.Should().Be(user.Email);
            result.Name.Should().Be(user.Username);
            result.Role.Should().Be(user.Role.ToString());
        }

        /// <summary>
        /// Tests that a missing user throws UnauthorizedAccessException with "Invalid credentials".
        /// </summary>
        [Fact]
        public async Task Handle_UserNotFound_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var command = new AuthenticateUserCommand { Email = "missing@example.com", Password = "x" };
            _userRepository
                .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<User?>(null));

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("Invalid credentials");
        }

        /// <summary>
        /// Tests that incorrect password throws UnauthorizedAccessException with "Invalid credentials".
        /// </summary>
        [Fact]
        public async Task Handle_WrongPassword_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var user = new User { Email = "u@u.com", Password = "hasher", Status = UserStatus.Active };
            var command = new AuthenticateUserCommand { Email = user.Email, Password = "bad" };
            _userRepository
                .GetByEmailAsync(user.Email, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<User?>(user));
            _passwordHasher
                .VerifyPassword(command.Password, user.Password)
                .Returns(false);

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("Invalid credentials");
        }

        /// <summary>
        /// Tests that inactive user throws UnauthorizedAccessException with "User is not active".
        /// </summary>
        [Fact]
        public async Task Handle_InactiveUser_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var user = new User { Email = "u@u.com", Password = "hasher", Status = UserStatus.Suspended };
            var command = new AuthenticateUserCommand { Email = user.Email, Password = "raw" };
            _userRepository
                .GetByEmailAsync(user.Email, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<User?>(user));
            _passwordHasher
                .VerifyPassword(command.Password, user.Password)
                .Returns(true);

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("User is not active");
        }
    }
}
