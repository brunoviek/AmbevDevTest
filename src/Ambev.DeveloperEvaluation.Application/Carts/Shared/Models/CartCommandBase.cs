using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.Shared.Models
{
    /// <summary>
    /// Base command containing data needed to create or update a cart.
    /// </summary>
    public abstract class CartCommandBase
    {
        /// <summary>
        /// Gets or sets the identifier of the user who owns the cart.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the date of the cart.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the collection of products with their quantities.
        /// </summary>
        public IList<CartProductModel> Products { get; set; } = new List<CartProductModel>();
    }
}
