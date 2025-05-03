using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Rebus.Bus;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Application.Products.Events;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    /// <summary>
    /// Handler responsible for creating a new product.
    /// </summary>
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of <see cref="CreateProductCommandHandler"/>.
        /// </summary>
        /// <param name="productRepository">Repository for product persistence.</param>
        /// <param name="mapper">AutoMapper instance for entity mapping.</param>
        /// <param name="bus">Rebus bus for event publishing.</param>
        public CreateProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper,
            IBus bus)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _bus = bus;
        }

        /// <summary>
        /// Handles the creation of a product:
        ///   1. Maps the request to the domain entity.
        ///   2. Persists the entity in the repository.
        ///   3. Publishes a <see cref="ProductCreatedEvent"/>.
        ///   4. Returns the created <see cref="ProductResult"/>.
        /// </summary>
        /// <param name="request">The command containing product data (id, title, price, description, category, image, rating).</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The result of the created product.</returns>
        public async Task<ProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Product>(request);

            var createdEntity = await _productRepository.AddAsync(productEntity, cancellationToken);

            var result = _mapper.Map<ProductResult>(createdEntity);

            await _bus.Publish(new ProductCreatedEvent(createdEntity));

            return result;
        }
    }
}
