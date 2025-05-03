using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.Events;
using Ambev.DeveloperEvaluation.Application.Carts.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using NSubstitute;
using Rebus.Bus;
using Xunit;
using NSubstitute.ExceptionExtensions;
using Ambev.DeveloperEvaluation.TestCommon.Carts;

namespace Ambev.DeveloperEvaluation.Tests.Application.Carts
{
    public class CreateCartCommandHandlerTests
    {
        private readonly ICartRepository _repository = Substitute.For<ICartRepository>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly IBus _bus = Substitute.For<IBus>();
        private readonly CreateCartCommandHandler _handler;

        public CreateCartCommandHandlerTests()
        {
            _handler = new CreateCartCommandHandler(_repository, _mapper, _bus);
        }

        [Fact]
        public async Task Handle_ValidCommand_CreatesCartAndPublishesEvent_ReturnsMappedResult()
        {
            // Arrange
            var command = CartsCommandTestData.GenerateCreate();
            var cartEntity = new Cart
            {
                Id = 123,
                UserId = command.UserId,
                Date = command.Date,
                Products = new List<CartItem>()
            };
            foreach (var p in command.Products)
            {
                cartEntity.Products.Add(new CartItem { ProductId = p.ProductId, Quantity = p.Quantity });
            }
            var resultDto = new CartResult
            {
                Id = cartEntity.Id,
                UserId = cartEntity.UserId,
                Date = cartEntity.Date,
                Products = new List<CartProductResult>()
            };
            foreach (var i in cartEntity.Products)
            {
                resultDto.Products.Add(new CartProductResult { ProductId = i.ProductId, Quantity = i.Quantity });
            }

            _mapper.Map<Cart>(command).Returns(cartEntity);
            _repository.AddAsync(cartEntity).Returns(Task.FromResult(cartEntity));
            _mapper.Map<CartResult>(cartEntity).Returns(resultDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(resultDto, result);
            await _repository.Received(1).AddAsync(cartEntity);
            await _bus.Received(1).Publish(Arg.Is<CartCreatedEvent>(e => e.Cart == cartEntity));
        }

        [Fact]
        public async Task Handle_RepositoryThrows_ThrowsExceptionAndDoesNotPublishEvent()
        {
            // Arrange
            var command = CartsCommandTestData.GenerateCreate();
            var cartEntity = new Cart { UserId = command.UserId, Date = command.Date };
            _mapper.Map<Cart>(command).Returns(cartEntity);
            _repository.AddAsync(cartEntity).ThrowsAsync(new InvalidOperationException("DB failure"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
            await _repository.Received(1).AddAsync(cartEntity);
            await _bus.DidNotReceive().Publish(Arg.Any<CartCreatedEvent>());
        }

        [Fact]
        public async Task Handle_BusPublishThrows_ThrowsExceptionAfterCreatingCart()
        {
            // Arrange
            var command = CartsCommandTestData.GenerateCreate();
            var cartEntity = new Cart
            {
                Id = 456,
                UserId = command.UserId,
                Date = command.Date,
                Products = new List<CartItem>()
            };
            foreach (var p in command.Products)
            {
                cartEntity.Products.Add(new CartItem { ProductId = p.ProductId, Quantity = p.Quantity });
            }

            _mapper.Map<Cart>(command).Returns(cartEntity);
            _repository.AddAsync(cartEntity).Returns(Task.FromResult(cartEntity));
            _bus.When(b => b.Publish(Arg.Any<CartCreatedEvent>()))
                .Do(call => throw new ApplicationException("Bus error"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Bus error", ex.Message);
            await _repository.Received(1).AddAsync(cartEntity);
            await _bus.Received(1).Publish(Arg.Any<CartCreatedEvent>());
        }
    }
}
