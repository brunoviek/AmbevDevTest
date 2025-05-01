using Ambev.DeveloperEvaluation.Application.Products.Shared.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared.Requets
{
    public abstract class ProductRequest
    {
        /// <summary>
        /// The product title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The product description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The category of the product.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// The Base64-encoded image data for the product.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// The product rating details.
        /// </summary>
        public RatingRequest Rating { get; set; } = null!;
    }
}
