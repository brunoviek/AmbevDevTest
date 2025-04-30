using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities.User;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.GetUser.Mappings;

/// <summary>
/// Contains tests for the GetUserMappingProfile to ensure User entities are correctly mapped to GetUserResult.
/// </summary>
public class GetUserMappingProfileTests
{
    private readonly IMapper _mapper;

    public GetUserMappingProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<GetUserProfile>();
        });

        _mapper = config.CreateMapper();
    }

    /// <summary>
    /// Tests that a valid User entity is correctly mapped to GetUserResult.
    /// </summary>
    [Fact(DisplayName = "Should correctly map User to GetUserResult")]
    public void Should_Map_User_To_GetUserResult_Correctly()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "johndoe",
            Email = "john.doe@example.com",
            Phone = "(11) 91234-5678",
            Name = new Name
            {
                Firstname = "John",
                Lastname = "Doe"
            },
            Address = new Address
            {
                Street = "Main St",
                Number = 123,
                City = "São Paulo",
                Zipcode = "12345-678",
                Geolocation = new Geolocation
                {
                    Latitude = "-23.5505",
                    Longitude = "-46.6333"
                }
            }
        };

        var result = _mapper.Map<UserResult>(user);

        result.Should().NotBeNull();
        result.Id.Should().Be(user.Id);
        result.Username.Should().Be(user.Username);
        result.Email.Should().Be(user.Email);
        result.Phone.Should().Be(user.Phone);

        result.Name.Should().NotBeNull();
        result.Name!.Firstname.Should().Be(user.Name.Firstname);
        result.Name.Lastname.Should().Be(user.Name.Lastname);

        result.Address.Should().NotBeNull();
        result.Address!.Street.Should().Be(user.Address.Street);
        result.Address.Number.Should().Be(user.Address.Number);
        result.Address.City.Should().Be(user.Address.City);
        result.Address.Zipcode.Should().Be(user.Address.Zipcode);

        result.Address.Geolocation.Should().NotBeNull();
        result.Address.Geolocation!.Latitude.Should().Be(user.Address.Geolocation.Latitude);
        result.Address.Geolocation.Longitude.Should().Be(user.Address.Geolocation.Longitude);
    }
}
