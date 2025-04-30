using FluentValidation;

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
        public DeleteProductValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
