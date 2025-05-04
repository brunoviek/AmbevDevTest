using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    /// <summary>
    /// Validator for <see cref="DeleteProductCommand"/>, ensuring the product ID is valid.
    /// </summary>
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DeleteProductValidator"/>, applying validation rules.
        /// </summary>
        public DeleteProductValidator(ICartRepository cartRepository)
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Id)
                .MustAsync(NoCartItemsReferencingProduct)
                .WithMessage("Cannot delete product because it exists in one or more cart items.")
                .WithErrorCode("ProductInUse");

            Task<bool> NoCartItemsReferencingProduct(int productId, CancellationToken ct)
            {
                var existsInCart = cartRepository.QueryAll().Any(c => c.Products.Any(item => item.ProductId == productId));
                return Task.FromResult(!existsInCart);
            }
        }
    }
}
