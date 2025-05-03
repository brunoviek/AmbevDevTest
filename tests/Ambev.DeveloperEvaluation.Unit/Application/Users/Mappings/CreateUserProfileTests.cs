using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using AutoMapper;
using Xunit;
using FluentAssertions;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;
using Ambev.DeveloperEvaluation.Common.Users;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.Mappings
{
    /// <summary>
    /// Contains tests for the <see cref="CreateUserProfile"/> to ensure commands and models
    /// are correctly mapped to domain entities and results using centralized test data.
    /// </summary>
    public class CreateUserProfileTests
    {
        private readonly IMapper _mapper;

        public CreateUserProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreateUserProfile>();
            });
            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// Tests that the Name model maps correctly.
        /// </summary>
        [Fact(DisplayName = "Should map UserNameModel to Name correctly")]
        public void Should_Map_CreateUserNameModel_To_Name()
        {
            var model = CreateUserHandlerTestData.GenerateNameModel();
            var entity = _mapper.Map<Name>(model);
            entity.Firstname.Should().Be(model.Firstname);
            entity.Lastname.Should().Be(model.Lastname);
        }

        /// <summary>
        /// Tests that the Geolocation model maps correctly.
        /// </summary>
        [Fact(DisplayName = "Should map UserGeolocationModel to Geolocation correctly")]
        public void Should_Map_CreateUserGeolocationModel_To_Geolocation()
        {
            var model = CreateUserHandlerTestData.GenerateGeolocationModel();
            var entity = _mapper.Map<Geolocation>(model);
            entity.Latitude.Should().Be(model.Latitude);
            entity.Longitude.Should().Be(model.Longitude);
        }

        /// <summary>
        /// Tests that the Address model maps correctly, including nested geolocation.
        /// </summary>
        [Fact(DisplayName = "Should map CreateUserAddressModel to Address correctly")]
        public void Should_Map_CreateUserAddressModel_To_Address()
        {
            var model = CreateUserHandlerTestData.GenerateAddressModel();
            var entity = _mapper.Map<Address>(model);
            entity.Street.Should().Be(model.Street);
            entity.Number.Should().Be(model.Number);
            entity.City.Should().Be(model.City);
            entity.Zipcode.Should().Be(model.Zipcode);
            entity.Geolocation.Latitude.Should().Be(model.Geolocation!.Latitude);
            entity.Geolocation.Longitude.Should().Be(model.Geolocation.Longitude);
        }

        /// <summary>
        /// Tests that a CreateUserCommand maps to a Products entity correctly, including nested models.
        /// </summary>
        [Fact(DisplayName = "Should map CreateUserCommand to Products correctly using test data")]
        public void Should_Map_CreateUserCommand_To_User()
        {
            var command = CreateUserHandlerTestData.GenerateValidCommand();
            var user = _mapper.Map<User>(command);

            user.Username.Should().Be(command.Username);
            user.Password.Should().Be(command.Password);
            user.Email.Should().Be(command.Email);
            user.Phone.Should().Be(command.Phone);
            user.Status.Should().Be(command.Status);
            user.Role.Should().Be(command.Role);

            user.Name!.Firstname.Should().Be(command.Name!.Firstname);
            user.Name.Lastname.Should().Be(command.Name.Lastname);

            user.Address!.Street.Should().Be(command.Address!.Street);
            user.Address.Number.Should().Be(command.Address.Number);
            user.Address.City.Should().Be(command.Address.City);
            user.Address.Zipcode.Should().Be(command.Address.Zipcode);
            user.Address.Geolocation!.Latitude.Should().Be(command.Address.Geolocation!.Latitude);
            user.Address.Geolocation.Longitude.Should().Be(command.Address.Geolocation.Longitude);
        }

        /// <summary>
        /// Tests that a Products entity maps to UserResult correctly.
        /// </summary>
        [Fact(DisplayName = "Should map Products to UserResult correctly")]
        public void Should_Map_User_To_UserResult()
        {
            var id = Guid.NewGuid();
            var user = new User { Id = id };
            var result = _mapper.Map<UserResult>(user);
            result.Id.Should().Be(id);
        }
    }
}
