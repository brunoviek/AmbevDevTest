using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.Events
{
    public class ProductDeletedEvent
    {
        public Product Product { get; }

        public ProductDeletedEvent(Product product)
        {
            Product = product;
        }
    }
}
