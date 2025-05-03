using Ambev.DeveloperEvaluation.Application.Carts.Shared.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.Shared.Requets
{
    public class CartRequest
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
        public IList<CartProductRequest> Products { get; set; } = new List<CartProductRequest>();
    }
}
