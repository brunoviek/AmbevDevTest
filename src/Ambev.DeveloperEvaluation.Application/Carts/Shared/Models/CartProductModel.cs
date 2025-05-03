using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.Shared.Models
{
    /// <summary>
    /// Represents a product entry within a cart, including its quantity.
    /// </summary>
    public class CartProductModel
    {
        /// <summary>
        /// Gets or sets the identifier of the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the cart.
        /// </summary>
        public int Quantity { get; set; }
    }
}
