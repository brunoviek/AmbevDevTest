namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared.Requets
{
    public class RatingRequest
    {
        /// <summary>
        /// The average rating value.
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// The number of ratings submitted.
        /// </summary>
        public int Count { get; set; }
    }
}
