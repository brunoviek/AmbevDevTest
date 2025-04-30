using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser.Models;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateUserHandlerTestData
{

    // Faker for Name model
    private static readonly Faker<CreateUserNameModel> nameFaker = new Faker<CreateUserNameModel>()
        .RuleFor(m => m.Firstname, f => f.Name.FirstName())
        .RuleFor(m => m.Lastname, f => f.Name.LastName());

    // Faker for Geolocation model
    private static readonly Faker<CreateUserGeolocationModel> geoFaker = new Faker<CreateUserGeolocationModel>()
        .RuleFor(m => m.Latitude, f => f.Address.Latitude().ToString())
        .RuleFor(m => m.Longitude, f => f.Address.Longitude().ToString());

    // Faker for Address model (includes geolocation)
    private static readonly Faker<CreateUserAddressModel> addressFaker = new Faker<CreateUserAddressModel>()
        .RuleFor(m => m.Street, f => f.Address.StreetName())
        .RuleFor(m => m.Number, f => f.Random.Number(1, 1000))
        .RuleFor(m => m.City, f => f.Address.City())
        .RuleFor(m => m.Zipcode, f => f.Address.ZipCode())
        .RuleFor(m => m.Geolocation, _ => geoFaker.Generate());

    // Faker for CreateUserCommand (includes name and address models)
    private static readonly Faker<CreateUserCommand> commandFaker = new Faker<CreateUserCommand>()
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
        .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
        .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
        .RuleFor(u => u.Name, _ => nameFaker.Generate())
        .RuleFor(u => u.Address, _ => addressFaker.Generate());

    /// <summary>
    /// Generates a valid <see cref="CreateUserNameModel"/>.
    /// </summary>
    public static CreateUserNameModel GenerateNameModel() => nameFaker.Generate();

    /// <summary>
    /// Generates a valid <see cref="CreateUserGeolocationModel"/>.
    /// </summary>
    public static CreateUserGeolocationModel GenerateGeolocationModel() => geoFaker.Generate();

    /// <summary>
    /// Generates a valid <see cref="CreateUserAddressModel"/>.
    /// </summary>
    public static CreateUserAddressModel GenerateAddressModel() => addressFaker.Generate();

    /// <summary>
    /// Generates a valid <see cref="CreateUserCommand"/> including nested name and address.
    /// </summary>
    public static CreateUserCommand GenerateValidCommand() => commandFaker.Generate();
}
