using System;
using FluentAssertions;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Common.Exceptions;

namespace Ambev.DeveloperEvaluation.Unit.Application.Common
{
    /// <summary>
    /// Unit tests for <see cref="OrderValidator"/>'s user-specific sorting validation.
    /// </summary>
    public class OrderValidatorTests
    {
        /// <summary>
        /// Tests that a null input returns null unchanged.
        /// </summary>
        [Fact]
        public void ValidateUserOrderFields_NullInput_ReturnsNull()
        {
            // Arrange
            string input = null!;

            // Act
            var result = OrderValidator.ValidateUserOrderFields(input);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Tests that an empty string input returns an empty string unchanged.
        /// </summary>
        [Fact]
        public void ValidateUserOrderFields_EmptyString_ReturnsEmptyString()
        {
            // Arrange
            var input = string.Empty;

            // Act
            var result = OrderValidator.ValidateUserOrderFields(input);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Tests that a whitespace-only string input returns the same whitespace string unchanged.
        /// </summary>
        [Fact]
        public void ValidateUserOrderFields_WhitespaceString_ReturnsSameString()
        {
            // Arrange
            var input = "   ";

            // Act
            var result = OrderValidator.ValidateUserOrderFields(input);

            // Assert
            result.Should().Be(input);
        }

        /// <summary>
        /// Tests that a valid single field expression returns the original expression.
        /// </summary>
        [Fact]
        public void ValidateUserOrderFields_ValidSingleField_ReturnsOriginal()
        {
            // Arrange
            var input = "Username";

            // Act
            var result = OrderValidator.ValidateUserOrderFields(input);

            // Assert
            result.Should().Be(input);
        }

        /// <summary>
        /// Tests that valid multiple-field expressions return the original expressions unchanged.
        /// </summary>
        [Fact]
        public void ValidateUserOrderFields_ValidMultipleFields_ReturnsOriginal()
        {
            // Arrange
            var inputs = new[]
            {
                "Email desc",
                "FirstName asc,LastName desc",
                " city , street "
            };

            // Act & Assert
            foreach (var expr in inputs)
            {
                var result = OrderValidator.ValidateUserOrderFields(expr);
                result.Should().Be(expr);
            }
        }

        /// <summary>
        /// Tests that invalid field expressions throw a <see cref="BadRequestException"/>.
        /// </summary>
        [Fact]
        public void ValidateUserOrderFields_InvalidField_ThrowsBadRequestException()
        {
            // Arrange
            var invalidInputs = new[]
            {
                "InvalidField",
                "Username,InvalidField asc",
                "Email desc, UnknownField desc"
            };

            // Act & Assert
            foreach (var expr in invalidInputs)
            {
                Action act = () => OrderValidator.ValidateUserOrderFields(expr);
                act.Should().Throw<BadRequestException>()
                   .WithMessage("Invalid ordering field: *");
            }
        }
    }
}
