using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.TestCommon.Carts
{
    /// <summary>
    /// Provides fake data for Cart and CartItem entities using Bogus.
    /// </summary>
    public static class CartsTestData
    {
        private static readonly Faker<CartItem> CartItemFaker = new Faker<CartItem>()
            .RuleFor(ci => ci.Id, f => f.IndexFaker + 1)
            .RuleFor(ci => ci.ProductId, f => f.Random.Int(1, 100))
            .RuleFor(ci => ci.Quantity, f => f.Random.Int(1, 10));

        /// <summary>
        /// Generates a single fake CartItem.
        /// </summary>
        public static CartItem GenerateCartItem() => CartItemFaker.Generate();

        /// <summary>
        /// Generates a list of fake CartItems.
        /// </summary>
        public static List<CartItem> GenerateCartItems(int count = 3) => CartItemFaker.Generate(count);

        private static readonly Faker<Cart> CartFaker = new Faker<Cart>()
            .RuleFor(c => c.Id, f => f.IndexFaker + 1)
            .RuleFor(c => c.UserId, f => f.Random.Guid())
            .RuleFor(c => c.Date, f => f.Date.Recent())
            .RuleFor(c => c.Products, f => GenerateCartItems(f.Random.Int(1, 5)));

        /// <summary>
        /// Generates a single fake Cart with items.
        /// </summary>
        public static Cart GenerateCart() => CartFaker.Generate();

        /// <summary>
        /// Generates a list of fake Carts with items.
        /// </summary>
        public static List<Cart> GenerateCarts(int count = 5) => CartFaker.Generate(count);
    }
}
