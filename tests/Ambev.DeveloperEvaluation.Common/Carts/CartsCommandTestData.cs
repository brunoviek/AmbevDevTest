using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.Shared.Models;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.TestCommon.Carts
{
    /// <summary>
    /// Provides fake data for Cart commands using Bogus.
    /// </summary>
    public static class CartsCommandTestData
    {
        private static readonly Faker<CartProductModel> CartProductDtoFaker = new Faker<CartProductModel>()
            .RuleFor(p => p.ProductId, f => f.Random.Int(1, 100))
            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 10));

        private static readonly Faker<CreateCartCommand> CreateCartCommandFaker = new Faker<CreateCartCommand>()
            .RuleFor(cmd => cmd.UserId, f => f.Random.Guid())
            .RuleFor(cmd => cmd.Date, f => f.Date.Recent())
            .RuleFor(cmd => cmd.Products, f => CartProductDtoFaker.Generate(f.Random.Int(1, 5)));

        private static readonly Faker<UpdateCartCommand> UpdateCartCommandFaker = new Faker<UpdateCartCommand>()
            .RuleFor(cmd => cmd.Id, f => f.Random.Int(1, 50))
            .RuleFor(cmd => cmd.UserId, f => f.Random.Guid())
            .RuleFor(cmd => cmd.Date, f => f.Date.Recent())
            .RuleFor(cmd => cmd.Products, f => CartProductDtoFaker.Generate(f.Random.Int(1, 5)));

        private static readonly Faker<DeleteCartCommand> DeleteCartCommandFaker = new Faker<DeleteCartCommand>()
            .CustomInstantiator(f => new DeleteCartCommand(f.Random.Int(1, 50)));

        /// <summary>
        /// Generates a single CreateCartCommand.
        /// </summary>
        public static CreateCartCommand GenerateCreate() => CreateCartCommandFaker.Generate();

        /// <summary>
        /// Generates multiple CreateCartCommands.
        /// </summary>
        public static List<CreateCartCommand> GenerateCreates(int count = 5)
            => CreateCartCommandFaker.Generate(count);

        /// <summary>
        /// Generates a single UpdateCartCommand.
        /// </summary>
        public static UpdateCartCommand GenerateUpdate() => UpdateCartCommandFaker.Generate();

        /// <summary>
        /// Generates multiple UpdateCartCommands.
        /// </summary>
        public static List<UpdateCartCommand> GenerateUpdates(int count = 5)
            => UpdateCartCommandFaker.Generate(count);

        /// <summary>
        /// Generates a single DeleteCartCommand.
        /// </summary>
        public static DeleteCartCommand GenerateDelete() => DeleteCartCommandFaker.Generate();

        /// <summary>
        /// Generates multiple DeleteCartCommands.
        /// </summary>
        public static List<DeleteCartCommand> GenerateDeletes(int count = 5)
            => DeleteCartCommandFaker.Generate(count);
    }
}
