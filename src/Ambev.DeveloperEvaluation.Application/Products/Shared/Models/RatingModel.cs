using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.Shared.Models
{
    public class RatingModel
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
