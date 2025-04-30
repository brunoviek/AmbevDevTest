using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    /// <summary>
    /// Handler for executing <see cref="GetProductQuery"/>, retrieving a product by its ID.
    /// </summary>
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResult>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="GetProductQueryHandler"/>.
        /// </summary>
        /// <param name="repository">The product repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public GetProductQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve a product by ID:
        /// </summary>
        /// <param name="request">The query containing the product ID.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The result containing product data.</returns>
        public async Task<ProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken)
                          ?? throw new KeyNotFoundException($"Product with ID {request.Id} not found.");

            return _mapper.Map<ProductResult>(entity);
        }
    }
}
