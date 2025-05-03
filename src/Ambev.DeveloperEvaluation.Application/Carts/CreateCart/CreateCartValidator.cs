using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    /// <summary>
    /// Validates incoming CreateCartCommand, ensuring products exist and quantities are valid.
    /// </summary>
    public class CreateCartValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartValidator(IProductRepository productRepository)
        {
            RuleFor(x => x.Products).NotEmpty();

            RuleForEach(x => x.Products).ChildRules(products =>
            {
                products.RuleFor(p => p.ProductId)
                        .GreaterThan(0)
                        .MustAsync(async (id, cancellation) =>
                            await productRepository.ExistsByIdAsync(id))
                        .WithMessage(p => $"Product with Id {p.ProductId} does not exist.");

                products.RuleFor(p => p.Quantity).GreaterThan(0);
            });
        }
    }
}
