using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.Shared;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    /// <summary>
    /// Validator for <see cref="UpdateProductValidator"/> ensuring all required fields (except Image) are present, valid, and title is unique.
    /// </summary>
    public class UpdateProductValidator : ProductCommandBaseValidator<UpdateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UpdateProductValidator"/>.
        /// </summary>
        /// <param name="repository">Repository to check for existing product titles.</param>
        public UpdateProductValidator(IProductRepository repository) : base(repository)
        {
        }
    }
}
