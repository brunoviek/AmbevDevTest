using Ambev.DeveloperEvaluation.Application.Products.Shared.Results;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared.Responses
{
    public class ProductResponse
    {
        /// <summary>
        /// The product Id.
        /// </summary>
        public int Id { get; set; }

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
        public RatingResponse Rating { get; set; } = null!;
    }
}
