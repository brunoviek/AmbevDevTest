using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.Events
{
    public class ProductCreatedEvent
    {
        public Product Product { get; }

        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }
    }
}
