using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProductsByCategory
{
    /// <summary>
    /// Handles listing products with pagination and optional ordering,
    /// applying validation rules and mapping to result DTOs.
    /// </summary>
    public class ListProductsByCategoryQueryHandler : IRequestHandler<ListProductsByCategoryQuery, PaginatedList<ProductResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="ListProductsByCategoryQueryHandler"/>.
        /// </summary>
        /// <param name="productRepository">The repository for product data access.</param>
        /// <param name="mapper">The AutoMapper instance for entity mapping.</param>
        public ListProductsByCategoryQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Processes the ListProductsQuery by validating ordering,
        /// applying sorting, performing pagination, and mapping entities to results.
        /// </summary>
        /// <param name="request">The query containing page, size, and order parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A <see cref="PaginatedList{ProductResult}"/> containing products mapped to ProductResult.
        /// </returns>
        public async Task<PaginatedList<ProductResult>> Handle(
            ListProductsByCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var query = _productRepository.QueryAll();

            if (!string.IsNullOrWhiteSpace(request.Order))
                query = query.OrderBy(OrderValidator.ValidateProductOrderFields(request.Order));

            return await PaginatedList<ProductResult>.CreateAsync(
                query.ProjectTo<ProductResult>(_mapper.ConfigurationProvider),
                request.Page,
                request.Size,
                cancellationToken);
        }
    }
}
