using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.TestHelper;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Carts.Shared.Models;
using Ambev.DeveloperEvaluation.TestCommon.Carts;

namespace Ambev.DeveloperEvaluation.Tests.Application.Carts
{
    /// <summary>
    /// Tests for AutoMapper profiles and FluentValidation validators related to Cart.
    /// </summary>
    public class CartProfilesValidatorsTests
    {
        private readonly IMapper _mapper;
        private CreateCartValidator _createValidator;
        private readonly DeleteCartValidator _deleteValidator;
        private readonly GetCartQueryValidator _getValidator;
        private readonly ListCartsQueryValidator _listValidator;

        public CartProfilesValidatorsTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(UpdateCartProfile).Assembly);
            });
            _mapper = config.CreateMapper();

            var productRepo = Substitute.For<IProductRepository>();
            _createValidator = new CreateCartValidator(productRepo);
            _deleteValidator = new DeleteCartValidator();
            _getValidator = new GetCartQueryValidator();
            _listValidator = new ListCartsQueryValidator();
        }

        [Fact]
        public void UpdateCartProfile_Maps_Command_To_Entity_Correctly()
        {
            var command = CartsCommandTestData.GenerateUpdate();
            var entity = _mapper.Map<Cart>(command);

            Assert.Equal(command.UserId, entity.UserId);
            Assert.Equal(command.Date, entity.Date);
            Assert.Equal(command.Products.Count, entity.Products.Count);
            Assert.Equal(command.Id, entity.Id);
            for (int i = 0; i < command.Products.Count; i++)
            {
                var model = command.Products[i];
                var item = entity.Products[i];
                Assert.Equal(model.ProductId, item.ProductId);
                Assert.Equal(model.Quantity, item.Quantity);
            }
        }

        [Fact]
        public async Task CreateCartValidator_EmptyProducts_Fails()
        {
            var cmd = new CreateCartCommand { UserId = Guid.NewGuid(), Date = DateTime.UtcNow, Products = new List<CartProductModel>() };
            var result = await _createValidator.TestValidateAsync(cmd);
            result.ShouldHaveValidationErrorFor(c => c.Products);
        }

        [Fact]
        public async Task CreateCartValidator_InvalidProductId_And_Quantity_Fail()
        {
            var invalid = new CartProductModel { ProductId = 0, Quantity = 0 };
            var cmd = new CreateCartCommand { UserId = Guid.NewGuid(), Date = DateTime.UtcNow, Products = new List<CartProductModel> { invalid } };
            var result = await _createValidator.TestValidateAsync(cmd);
            result.ShouldHaveValidationErrorFor("Products[0].ProductId");
            result.ShouldHaveValidationErrorFor("Products[0].Quantity");
        }

        [Fact]
        public async Task CreateCartValidator_ProductDoesNotExist_Fails()
        {
            var prodId = 999;
            var repo = (IProductRepository)Substitute.For<IProductRepository>();
            repo.ExistsByIdAsync(prodId, Arg.Any<CancellationToken>()).Returns(false);

            var cmd = new CreateCartCommand { UserId = Guid.NewGuid(), Date = DateTime.UtcNow, Products = new List<CartProductModel> { new CartProductModel { ProductId = prodId, Quantity = 1 } } };
            var result = await _createValidator.TestValidateAsync(cmd);
            result.ShouldHaveValidationErrorFor("Products[0].ProductId");
        }

        [Fact]
        public async Task CreateCartValidator_ValidCommand_Passes()
        {
            var prodId = 5;
            var repo = Substitute.For<IProductRepository>();
            repo.ExistsByIdAsync(prodId, Arg.Any<CancellationToken>())
                .Returns(true);

            // sobrescreve o stub que veio do construtor
            _createValidator = new CreateCartValidator(repo);

            var cmd = new CreateCartCommand
            {
                UserId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Products = new List<CartProductModel> {
            new CartProductModel { ProductId = prodId, Quantity = 2 }
        }
            };

            // Act
            var result = await _createValidator.TestValidateAsync(cmd);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void DeleteCartValidator_InvalidId_Fails(int id)
        {
            var cmd = new DeleteCartCommand(id);
            var result = _deleteValidator.TestValidate(cmd);
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void DeleteCartValidator_ValidId_Passes()
        {
            var result = _deleteValidator.TestValidate(new DeleteCartCommand(10));
            result.ShouldNotHaveValidationErrorFor(c => c.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void GetCartQueryValidator_InvalidId_Fails(int id)
        {
            var q = new GetCartQuery(id);
            var result = _getValidator.TestValidate(q);
            result.ShouldHaveValidationErrorFor(q => q.Id);
        }

        [Fact]
        public void GetCartQueryValidator_ValidId_Passes()
        {
            var result = _getValidator.TestValidate(new GetCartQuery(5));
            result.ShouldNotHaveValidationErrorFor(q => q.Id);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(1, 0)]
        [InlineData(1, 25)]
        public void ListCartsQueryValidator_InvalidParameters_Fail(int page, int size)
        {
            var q = new ListCartsQuery { Page = page, Size = size, Order = "" };
            var result = _listValidator.TestValidate(q);
            if (page <= 0) result.ShouldHaveValidationErrorFor(q => q.Page);
            if (size <= 0 || size > 20) result.ShouldHaveValidationErrorFor(q => q.Size);
        }

        [Fact]
        public void ListCartsQueryValidator_ValidParameters_Pass()
        {
            var q = new ListCartsQuery { Page = 2, Size = 10, Order = "id desc,userId asc" };
            var result = _listValidator.TestValidate(q);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
