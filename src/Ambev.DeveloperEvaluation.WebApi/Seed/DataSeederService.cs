using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Models;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;

namespace Ambev.DeveloperEvaluation.WebApi.Seed
{
    /// <summary>
    /// Service responsible for seeding initial data for testing purposes.
    /// </summary>
    public class DataSeederService
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public DataSeederService(
            IMediator mediator,
            IUserRepository userRepository,
            IProductRepository productRepository,
            ICartRepository cartRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        /// <summary>
        /// Executes the data seeding process.
        /// </summary>
        public async Task SeedAsync()
        {
            await SeedUsersAsync();
            await SeedProductsAsync();
            await SeedCartsAsync();
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

        private async Task SeedProductsAsync()
        {
            var products = new[]
            {
                new { Title = "Widget Alpha", Price = 9.99m,  Description = "First widget",   Category = "Widgets", Image = "/img/a.png", Rate = 4.2, Count = 15 },
                new { Title = "Widget Beta",  Price = 14.50m, Description = "Second widget",  Category = "Widgets", Image = "/img/b.png", Rate = 4.5, Count = 10 },
                new { Title = "Gadget Gamma", Price = 19.75m, Description = "Premium gadget", Category = "Gadgets", Image = "/img/c.png", Rate = 4.7, Count =  8 },
                new { Title = "Gadget Delta", Price = 24.00m, Description = "Deluxe gadget",  Category = "Gadgets", Image = "/img/d.png", Rate = 4.1, Count = 20 },
                new { Title = "Tool Epsilon", Price = 29.99m, Description = "Handy tool",     Category = "Tools",   Image = "/img/e.png", Rate = 4.3, Count = 12 },
            };

            foreach (var pd in products)
            {
                if (!await _productRepository.ExistsByTitleAsync(pd.Title))
                {
                    var product = new Product
                    {
                        Title = pd.Title,
                        Price = pd.Price,
                        Description = pd.Description,
                        Category = pd.Category,
                        Image = pd.Image,
                        Rating = new Rating { Rate = pd.Rate, Count = pd.Count }
                    };
                    await _productRepository.AddAsync(product);
                }
            }
        }

        private async Task SeedCartsAsync()
        {
            var user = await _userRepository.GetByEmailAsync("user@user.com");
            if (user == null) return;

            var hasCart = await _cartRepository
                .QueryAll()
                .AnyAsync(c => c.UserId == user.Id);
            if (hasCart) return;

            var products = await _productRepository
                .QueryAll()
                .Take(3)
                .ToListAsync();

            var items = products.Select(p => new CartItem
            {
                ProductId = p.Id,
                Quantity = 1,
            }).ToList();

            var cart = new Cart
            {
                UserId = user.Id,
                Date = DateTime.UtcNow,
                Products = items
            };

            await _cartRepository.AddAsync(cart);
        }
    }
}
