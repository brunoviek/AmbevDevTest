using Ambev.DeveloperEvaluation.Application.Carts.Events;
using Ambev.DeveloperEvaluation.Application.Carts.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Rebus.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, CartResult>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of <see cref="CreateCartCommandHandler"/>.
        /// </summary>
        public CreateCartCommandHandler(
            ICartRepository repository,
            IMapper mapper,
            IBus bus)
        {
            _repository = repository;
            _mapper = mapper;
            _bus = bus;
        }

        /// <inheritdoc/>
        public async Task<CartResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = _mapper.Map<Cart>(request);
            var created = await _repository.AddAsync(cart);
            await _bus.Publish(new CartCreatedEvent(created));
            return _mapper.Map<CartResult>(created);
        }
    }
}
