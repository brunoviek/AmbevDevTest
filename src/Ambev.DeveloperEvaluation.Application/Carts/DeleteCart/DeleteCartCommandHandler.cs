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

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    /// <summary>
    /// Handles the deletion of a cart.
    /// </summary>
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, string>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of <see cref="DeleteCartCommandHandler"/>.
        /// </summary>
        public DeleteCartCommandHandler(
            ICartRepository repository,
            IMapper mapper,
            IBus bus)
        {
            _repository = repository;
            _mapper = mapper;
            _bus = bus;
        }

        /// <summary>
        /// Handles the deletion of a cart:
        /// 1. Retrieves the existing cart.
        /// 2. Deletes it from the repository.
        /// 3. Publishes a <see cref="CartDeletedEvent"/>.
        /// 4. Maps the deleted entity to <see cref="CartResult"/>.
        /// </summary>
        public async Task<string> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Cart with ID {request.Id} not found.");

            await _repository.DeleteAsync(request.Id);

            var result = _mapper.Map<CartResult>(existing);

            await _bus.Publish(new CartDeletedEvent(existing));

            return $"Cart {request.Id} deleted successfully.";
        }
    }
}
