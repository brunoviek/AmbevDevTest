using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Products.Shared;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    /// <summary>
    /// Validator for <see cref="CreateProductCommand"/> ensuring all required fields (except Image) are present, valid, and title is unique.
    /// </summary>
    public class CreateProductValidator : ProductCommandBaseValidator<CreateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CreateProductValidator"/>.
        /// </summary>
        /// <param name="repository">Repository to check for existing product titles.</param>
        public CreateProductValidator(IProductRepository repository) : base(repository)
        {
        }
    }
}
