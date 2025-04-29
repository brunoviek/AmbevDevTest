using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the UserValidator class.
/// Tests cover validation of all user properties including username, email,
/// password, phone, status, and role requirements.
/// </summary>
public class UserValidatorTests
{
    private readonly UserValidator _validator;

    public UserValidatorTests()
    {
        _validator = new UserValidator();
    }

    /// <summary>
    /// Tests that validation passes when all user properties are valid.
    /// This test verifies that a user with valid:
    /// - Username (3-50 characters)
    /// - Password (meets complexity requirements)
    /// - Email (valid format)
    /// - Phone (valid Brazilian format)
    /// - Status (Active/Suspended)
    /// - Role (Customer/Admin)
    /// passes all validation rules without any errors.
    /// </summary>
    [Fact(DisplayName = "Valid user should pass all validation rules")]
    public void Given_ValidUser_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();

        // Act
        var result = _validator.TestValidate(user);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails for invalid username formats.
    /// This test verifies that usernames that are:
    /// - Empty strings
    /// - Less than 3 characters
    /// fail validation with appropriate error messages.
    /// The username is a required field and must be between 3 and 50 characters.
    /// </summary>
    /// <param name="username">The invalid username to test.</param>
    [Theory(DisplayName = "Invalid username formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    public void Given_InvalidUsername_When_Validated_Then_ShouldHaveError(string username)
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();
        user.Username = username;

        // Act
        var result = _validator.TestValidate(user);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }

    /// <summary>
    /// Tests that validation fails when username exceeds maximum length.
    /// This test verifies that usernames longer than 50 characters fail validation.
    /// The test uses TestDataGenerator to create a username that exceeds the maximum
    /// length limit, ensuring the validation rule is properly enforced.
    /// </summary>
    [Fact(DisplayName = "Username longer than maximum length should fail validation")]
    public void Given_UsernameLongerThanMaximum_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();
        user.Username = UserTestData.GenerateLongUsername();

        // Act
        var result = _validator.TestValidate(user);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }

    /// <summary>
    /// Tests that validation fails for invalid email formats.
    /// This test verifies that emails that:
    /// - Don't follow the standard email format (user@domain.com)
    /// - Don't contain @ symbol
    /// - Don't have a valid domain part
    /// fail validation with appropriate error messages.
    /// The test uses TestDataGenerator to create invalid email formats.
    /// </summary>
    [Fact(DisplayName = "Invalid email formats should fail validation")]
    public void Given_InvalidEmail_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();
        user.Email = UserTestData.GenerateInvalidEmail();

        // Act
        var result = _validator.TestValidate(user);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    /// <summary>
    /// Tests that validation fails for invalid password formats.
    /// This test verifies that passwords that don't meet the complexity requirements:
    /// - Minimum length of 8 characters
    /// - At least one uppercase letter
    /// - At least one lowercase letter
    /// - At least one number
    /// - At least one special character
    /// fail validation with appropriate error messages.
    /// The test uses TestDataGenerator to create passwords that don't meet these requirements.
    /// </summary>
    [Fact(DisplayName = "Invalid password formats should fail validation")]
    public void Given_InvalidPassword_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();
        user.Password = UserTestData.GenerateInvalidPassword();

        // Act
        var result = _validator.TestValidate(user);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    /// <summary>
    /// Tests that validation fails for invalid phone formats.
    /// This test verifies that phone numbers that:
    /// - Don't follow the Brazilian phone number format (+55XXXXXXXXXXXX)
    /// - Don't have the correct length
    /// - Don't start with the country code (+55)
    /// fail validation with appropriate error messages.
    /// The test uses TestDataGenerator to create invalid phone number formats.
    /// </summary>
    [Fact(DisplayName = "Invalid phone formats should fail validation")]
    public void Given_InvalidPhone_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();
        user.Phone = UserTestData.GenerateInvalidPhone();

        // Act
        var result = _validator.TestValidate(user);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Phone);
    }

    /// <summary>
    /// Tests that validation fails when user status is Unknown.
    /// This test verifies that:
    /// - The UserStatus cannot be set to Unknown
    /// - Only Active or Suspended are valid status values
    /// The test ensures that the system maintains valid user states
    /// and prevents undefined or invalid status values.
    /// </summary>
    [Fact(DisplayName = "Unknown status should fail validation")]
    public void Given_UnknownStatus_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();
        user.Status = UserStatus.Unknown;

        // Act
        var result = _validator.TestValidate(user);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Status);
    }

    /// <summary>
    /// Tests that validation fails when user role is None.
    /// This test verifies that:
    /// - The UserRole cannot be set to None
    /// - Only Customer or Admin are valid role values
    /// The test ensures that every user must have a defined role
    /// in the system and prevents undefined or invalid role assignments.
    /// </summary>
    [Fact(DisplayName = "None role should fail validation")]
    public void Given_NoneRole_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();
        user.Role = UserRole.None;

        // Act
        var result = _validator.TestValidate(user);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Role);
    }

    /// <summary>
    /// Validates that the user fails validation when Latitude is provided 
    /// in an invalid format (non-numeric string).
    /// Expected: Validation should fail for Address.Geolocation.Lat.
    /// </summary>
    [Fact(DisplayName = "Should fail when Latitude has invalid format (non-numeric)")]
    public void Should_Fail_When_Latitude_Is_Invalid_Format()
    {
        var user = UserTestData.GenerateValidUser();
        user.Address!.Geolocation.Latitude = "invalid_latitude";

        var result = _validator.TestValidate(user);

        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(x => x.Address!.Geolocation.Latitude);
    }

    /// <summary>
    /// Validates that the user fails validation when Latitude is provided 
    /// with a numeric value outside the valid range (-90 to 90).
    /// Expected: Validation should fail for Address.Geolocation.Lat.
    /// </summary>
    [Fact(DisplayName = "Should fail when Latitude value is out of valid range (-90 to 90)")]
    public void Should_Fail_When_Latitude_Is_Out_Of_Range()
    {
        var user = UserTestData.GenerateValidUser();
        user.Address!.Geolocation.Latitude = "91";

        var result = _validator.TestValidate(user);

        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(x => x.Address!.Geolocation.Latitude);
    }

    /// <summary>
    /// Validates that the user fails validation when Longitude is provided 
    /// in an invalid format (non-numeric string).
    /// Expected: Validation should fail for Address.Geolocation.Long.
    /// </summary>
    [Fact(DisplayName = "Should fail when Longitude has invalid format (non-numeric)")]
    public void Should_Fail_When_Longitude_Is_Invalid_Format()
    {
        var user = UserTestData.GenerateValidUser();
        user.Address!.Geolocation.Longitude = "invalid_longitude";

        var result = _validator.TestValidate(user);

        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(x => x.Address!.Geolocation.Longitude);
    }

    /// <summary>
    /// Validates that the user fails validation when Longitude is provided 
    /// with a numeric value outside the valid range (-180 to 180).
    /// Expected: Validation should fail for Address.Geolocation.Long.
    /// </summary>
    [Fact(DisplayName = "Should fail when Longitude value is out of valid range (-180 to 180)")]
    public void Should_Fail_When_Longitude_Is_Out_Of_Range()
    {
        var user = UserTestData.GenerateValidUser();
        user.Address!.Geolocation.Longitude = "200";

        var result = _validator.TestValidate(user);

        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(x => x.Address!.Geolocation.Longitude);
    }

    /// <summary>
    /// Validates that the user fails validation when Street is informed 
    /// but other Address fields (City, Zipcode, Number, Geolocation) are missing or invalid.
    /// Expected: Validation should fail for City, Zipcode, Number, Latitude, and Longitude.
    /// </summary>
    [Fact(DisplayName = "Should fail when Street is filled but other Address fields are missing")]
    public void Should_Fail_When_Street_Is_Filled_But_Other_Address_Fields_Are_Missing()
    {
        var user = UserTestData.GenerateValidUser();
        user.Address!.City = "";
        user.Address.Zipcode = "";
        user.Address.Number = 0;
        user.Address.Geolocation.Latitude = "";
        user.Address.Geolocation.Longitude = "";

        var result = _validator.TestValidate(user);

        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(x => x.Address!.City);
        result.ShouldHaveValidationErrorFor(x => x.Address!.Zipcode);
        result.ShouldHaveValidationErrorFor(x => x.Address!.Number);
        result.ShouldHaveValidationErrorFor(x => x.Address!.Geolocation.Latitude);
        result.ShouldHaveValidationErrorFor(x => x.Address!.Geolocation.Longitude);
    }

    /// <summary>
    /// Validates that the user fails validation when only City is filled 
    /// and other Address fields (Street, Zipcode, Number, Geolocation) are missing or invalid.
    /// Expected: Validation should fail for Street, Zipcode, Number, Latitude, and Longitude.
    /// </summary>
    [Fact(DisplayName = "Should fail when City is filled but other Address fields are missing")]
    public void Should_Fail_When_City_Is_Filled_But_Other_Address_Fields_Are_Missing()
    {
        var user = UserTestData.GenerateValidUser();
        user.Address!.Street = "";
        user.Address.Zipcode = "";
        user.Address.Number = 0;
        user.Address.Geolocation.Latitude = "";
        user.Address.Geolocation.Longitude = "";

        var result = _validator.TestValidate(user);

        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(x => x.Address!.Street);
        result.ShouldHaveValidationErrorFor(x => x.Address!.Zipcode);
        result.ShouldHaveValidationErrorFor(x => x.Address!.Number);
        result.ShouldHaveValidationErrorFor(x => x.Address!.Geolocation.Latitude);
        result.ShouldHaveValidationErrorFor(x => x.Address!.Geolocation.Longitude);
    }

    /// <summary>
    /// Validates that the user fails validation when only Number is filled 
    /// and other Address fields (Street, City, Zipcode, Geolocation) are missing or invalid.
    /// Expected: Validation should fail for Street, City, Zipcode, Latitude, and Longitude.
    /// </summary>
    [Fact(DisplayName = "Should fail when only Number is filled in Address")]
    public void Should_Fail_When_Only_Number_Is_Filled_In_Address()
    {
        var user = UserTestData.GenerateValidUser();
        user.Address!.Street = "";
        user.Address.City = "";
        user.Address.Zipcode = "";
        user.Address.Geolocation.Latitude = "";
        user.Address.Geolocation.Longitude = "";

        var result = _validator.TestValidate(user);

        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(x => x.Address!.Street);
        result.ShouldHaveValidationErrorFor(x => x.Address!.City);
        result.ShouldHaveValidationErrorFor(x => x.Address!.Zipcode);
        result.ShouldHaveValidationErrorFor(x => x.Address!.Geolocation.Latitude);
        result.ShouldHaveValidationErrorFor(x => x.Address!.Geolocation.Longitude);
    }

    /// <summary>
    /// Validates that the user passes validation when Address is completely and correctly filled.
    /// Expected: Validation should succeed with all Address fields correctly populated.
    /// </summary>
    [Fact(DisplayName = "Should pass when Address is completely filled")]
    public void Should_Pass_When_Address_Is_Completely_Filled()
    {
        var user = UserTestData.GenerateValidUser();

        var result = _validator.TestValidate(user);

        result.IsValid.Should().BeTrue();
    }


}
