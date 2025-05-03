using Ambev.DeveloperEvaluation.Application.Carts.Shared.Results;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    /// <summary>
    /// Handles listing carts with pagination and optional ordering.
    /// </summary>
    public class ListCartsQueryHandler : IRequestHandler<ListCartsQuery, PaginatedList<CartResult>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="ListCartsQueryHandler"/>.
        /// </summary>
        public ListCartsQueryHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Processes the ListCartsQuery by applying optional sorting and pagination.
        /// </summary>
        public async Task<PaginatedList<CartResult>> Handle(
            ListCartsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _cartRepository.QueryAll();

            if (!string.IsNullOrWhiteSpace(request.Order))
                query = query.OrderBy(request.Order);

            return await PaginatedList<CartResult>.CreateAsync(
                query.ProjectTo<CartResult>(_mapper.ConfigurationProvider),
                request.Page,
                request.Size,
                cancellationToken);
        }
    }
}
