using MediatR;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Seed
{
    /// <summary>
    /// Service responsible for seeding initial data for testing purposes.
    /// </summary>
    public class DataSeederService
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public DataSeederService(IMediator mediator, IUserRepository repository)
        {
            _mediator = mediator;
            _userRepository = repository;
        }

        /// <summary>
        /// Executes the data seeding process.
        /// </summary>
        public async Task SeedAsync()
        {
            await SeedUsersAsync();
        }

        private async Task SeedUsersAsync()
        {
            var existingAdmin = await _userRepository.GetByEmailAsync("admin@admin.com");
            if (existingAdmin is null)
            {
                await _mediator.Send(new CreateUserCommand
                {
                    Username = "admin",
                    Password = "Admin@123",
                    Email = "admin@admin.com",
                    Phone = "47999756127",
                    Role = UserRole.Admin,
                    Status = UserStatus.Active,
                    Name = new UserNameModel
                    {
                        Firstname = "Admin",
                        Lastname = "Products"
                    },
                    Address = new UserAddressModel
                    {
                        Street = "Admin Street",
                        Number = 100,
                        City = "Admin City",
                        Zipcode = "12345-678",
                        Geolocation = new UserGeolocationModel
                        {
                            Latitude = "-23.5505",
                            Longitude = "-46.6333"
                        }
                    }
                });
            }

            var existingCustomer = await _userRepository.GetByEmailAsync("user@user.com");
            if (existingCustomer is null)
            {
                await _mediator.Send(new CreateUserCommand
                {
                    Username = "user",
                    Password = "Products@123",
                    Email = "user@user.com",
                    Phone = "47999756127",
                    Role = UserRole.Customer,
                    Status = UserStatus.Active,
                    Name = new UserNameModel
                    {
                        Firstname = "John",
                        Lastname = "Doe"
                    },
                    Address = new UserAddressModel
                    {
                        Street = "Customer Street",
                        Number = 200,
                        City = "Products City",
                        Zipcode = "98765-432",
                        Geolocation = new UserGeolocationModel
                        {
                            Latitude = "-22.9068",
                            Longitude = "-43.1729"
                        }
                    }
                });
            }

            var existingCustomer2 = await _userRepository.GetByEmailAsync("user2@user.com");
            if (existingCustomer2 is null)
            {
                await _mediator.Send(new CreateUserCommand
                {
                    Username = "user2",
                    Password = "Products@123",
                    Email = "user2@user.com",
                    Phone = "47999756127",
                    Role = UserRole.Customer,
                    Status = UserStatus.Active,
                    Name = new UserNameModel
                    {
                        Firstname = "Jane",
                        Lastname = "Smith"
                    },
                    Address = new UserAddressModel
                    {
                        Street = "Another Street",
                        Number = 300,
                        City = "Another City",
                        Zipcode = "11111-222",
                        Geolocation = new UserGeolocationModel
                        {
                            Latitude = "-15.7801",
                            Longitude = "-47.9292"
                        }
                    }
                });
            }
        }
    }
}
