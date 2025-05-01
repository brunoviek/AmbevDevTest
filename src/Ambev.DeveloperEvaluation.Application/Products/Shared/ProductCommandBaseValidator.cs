using Ambev.DeveloperEvaluation.Application.Products.Shared.Models;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.Shared
{
    public abstract class ProductCommandBaseValidator<T> : AbstractValidator<T>
        where T : ProductCommandBase
    {
        protected ProductCommandBaseValidator(IProductRepository repository)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MustAsync(async (title, ct) => !await repository.ExistsByTitleAsync(title, ct))
                .WithMessage(title => $"A product with the title '{title.Title}' already exists.");
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Rating).NotNull();
            When(x => x.Rating != null, () =>
            {
                RuleFor(x => x.Rating.Rate).GreaterThan(0);
                RuleFor(x => x.Rating.Count).GreaterThanOrEqualTo(0);
            });
        }
    }
}
