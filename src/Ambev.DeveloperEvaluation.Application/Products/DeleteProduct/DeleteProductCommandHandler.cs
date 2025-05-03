using AutoMapper;
using MediatR;
using Rebus.Bus;
using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Products.Events;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    /// <summary>
    /// Handler responsible for deleting an existing product.
    /// </summary>
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductResult>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductCommandHandler"/> class.
        /// </summary>
        /// <param name="repository">Repository for product persistence.</param>
        /// <param name="mapper">AutoMapper instance for entity mapping.</param>
        /// <param name="bus">Rebus bus for event publishing.</param>
        public DeleteProductCommandHandler(
            IProductRepository repository,
            IMapper mapper,
            IBus bus)
        {
            _repository = repository;
            _mapper = mapper;
            _bus = bus;
        }

        /// <summary>
        /// Handles the deletion of a product:
        /// </summary>
        /// <param name="request">The command containing the product ID to delete.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The result of the deleted product.</returns>
        public async Task<ProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Products with ID {request.Id} not found.");

            await _repository.DeleteAsync(request.Id, cancellationToken);

            var result = _mapper.Map<ProductResult>(existing);

            await _bus.Publish(new ProductDeletedEvent(existing));

            return result;
        }
    }
}