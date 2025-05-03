namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.Shared.Responses
{
    /// <summary>
    /// Represents a single product entry in a cart result, including its quantity.
    /// </summary>
    public class CartProductResponse
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
