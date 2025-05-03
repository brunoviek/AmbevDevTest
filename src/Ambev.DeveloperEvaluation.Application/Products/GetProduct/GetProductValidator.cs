using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    /// <summary>
    /// Validator for <see cref="GetProductQuery"/>, ensuring the query contains a valid product identifier.
    /// </summary>
    public class GetProductValidator : AbstractValidator<GetProductQuery>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GetProductValidator"/>.
        /// </summary>
        public GetProductValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
