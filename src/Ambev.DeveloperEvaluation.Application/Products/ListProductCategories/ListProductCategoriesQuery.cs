using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProductCategories
{
    /// <summary>
    /// Query to retrieve all unique product categories.
    /// </summary>
    public class ListProductCategoriesQuery : IRequest<List<string>>
    {
    }
}