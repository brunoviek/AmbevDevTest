using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.Events
{
    public class ProductUpdatedEvent
    {
        public Product Product { get; }

        public ProductUpdatedEvent(Product product)
        {
            Product = product;
        }
    }
}
