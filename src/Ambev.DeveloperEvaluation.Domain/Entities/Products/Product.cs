using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Products
{
    /// <summary>
    /// Represents a product in the catalog.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The unique identifier for the product.
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
        public Rating Rating { get; set; } = null!;
    }
}
