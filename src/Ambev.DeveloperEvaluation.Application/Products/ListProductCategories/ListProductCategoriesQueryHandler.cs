using MediatR;
using Microsoft.EntityFrameworkCore;                   
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProductCategories
{
    /// <summary>
    /// Handler for ListProductCategoriesQuery.
    /// Retrieves all unique product categories using a projection on the repository's IQueryable.
    /// </summary>
    public class ListProductCategoriesQueryHandler : IRequestHandler<ListProductCategoriesQuery, List<string>>
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListProductCategoriesQueryHandler"/>.
        /// </summary>
        public ListProductCategoriesQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Handles the query to list all unique product categories.
        /// </summary>
        public async Task<List<string>> Handle(ListProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _productRepository
                .QueryAll()                                  
                .Select(p => p.Category)                     
                .Where(c => !string.IsNullOrWhiteSpace(c))   
                .Distinct()                                  
                .ToListAsync(cancellationToken);             

            return categories;
        }
    }
}
