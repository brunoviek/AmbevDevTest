using Ambev.DeveloperEvaluation.Application.Products.Events;
using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    /// <summary>
    /// Handler responsible for updating an existing product.
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of <see cref="UpdateProductCommandHandler"/>.
        /// </summary>
        /// <param name="productRepository">Repository for product persistence.</param>
        /// <param name="mapper">AutoMapper instance for entity mapping.</param>
        /// <param name="bus">Rebus bus for event publishing.</param>
        public UpdateProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper,
            IBus bus)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _bus = bus;
        }

        /// <summary>
        /// Handles the update of a product:
        /// </summary>
        /// <param name="request">The command containing updated product data (id, title, price, description, category, image, rating).</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The result of the updated product.</returns>
        public async Task<ProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await _productRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Products with ID {request.Id} not found.");

            _mapper.Map(request, existing);

            await _productRepository.UpdateAsync(existing, cancellationToken);

            var result = _mapper.Map<ProductResult>(existing);

            await _bus.Publish(new ProductUpdatedEvent(existing));

            return result;
        }
    }
}
