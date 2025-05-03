using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.Events
{
    public class CartCreatedEvent
    {
        public CartCreatedEvent(Cart cart)
        {
            Cart = cart;
        }

        public Cart Cart { get; set; }
    }
}
