using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    /// <summary>
    /// Validator for <see cref="DeleteCartCommand"/>, ensuring the cart ID is valid.
    /// </summary>
    public class DeleteCartValidator : AbstractValidator<DeleteCartCommand>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DeleteCartValidator"/>, applying validation rules.
        /// </summary>
        public DeleteCartValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
