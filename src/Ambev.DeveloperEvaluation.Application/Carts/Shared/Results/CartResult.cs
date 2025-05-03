using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.Shared.Results
{
    /// <summary>
    /// Represents the data returned after creating, updating or retrieving a cart.
    /// </summary>
    public class CartResult
    {
        /// <summary>
        /// Gets or sets the cart identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who owns the cart.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the date of the cart.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the collection of products in the cart.
        /// </summary>
        public IList<CartProductResult> Products { get; set; } = new List<CartProductResult>();
    }
}
