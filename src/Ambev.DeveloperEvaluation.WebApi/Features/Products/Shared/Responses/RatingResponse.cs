namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared.Responses
{
    public class RatingResponse
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
