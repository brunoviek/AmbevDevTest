using Ambev.DeveloperEvaluation.Application.Carts.Events;
using Ambev.DeveloperEvaluation.Application.Carts.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Rebus.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    /// <summary>
    /// Handler responsible for updating an existing cart.
    /// </summary>
    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, CartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of <see cref="UpdateCartCommandHandler"/>.
        /// </summary>
        public UpdateCartCommandHandler(
            ICartRepository cartRepository,
            IMapper mapper,
            IBus bus)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _bus = bus;
        }

        /// <summary>
        /// Handles the update of a cart
        /// </summary>
        public async Task<CartResult> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var existing = await _cartRepository.GetByIdAsync(request.Id)
                           ?? throw new KeyNotFoundException($"Cart with ID {request.Id} not found.");

            _mapper.Map(request, existing);

            await _cartRepository.UpdateAsync(existing);

            var result = _mapper.Map<CartResult>(existing);

            await _bus.Publish(new CartUpdatedEvent(existing));

            return result;
        }
    }
}
