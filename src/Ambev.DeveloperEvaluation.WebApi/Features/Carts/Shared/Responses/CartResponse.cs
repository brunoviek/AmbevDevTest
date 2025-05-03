using Ambev.DeveloperEvaluation.Application.Carts.Shared.Results;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.Shared.Responses
{
    /// <summary>
    /// Represents the data returned after creating, updating or retrieving a cart.
    /// </summary>
    public class CartResponse
    {
        /// <summary>
        /// Gets or sets the cart identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who owns the cart.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the date of the cart.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the collection of products in the cart.
        /// </summary>
        public IList<CartProductResponse> Products { get; set; } = new List<CartProductResponse>();
    }
}
