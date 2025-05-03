using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.Shared.Results
{
    /// <summary>
    /// Represents a single product entry in a cart result, including its quantity.
    /// </summary>
    public class CartProductResult
    {
        /// <summary>
        /// Gets or sets the identifier of the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of that product in the cart.
        /// </summary>
        public int Quantity { get; set; }
    }
}
