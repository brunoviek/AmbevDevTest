using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Common
{
    public abstract class ListQueryBase
    {
        /// <summary>
        /// Gets or sets the page number for pagination.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int Size { get; set; } = 10;

        /// <summary>
        /// Gets or sets the ordering criteria (e.g., "title asc, price desc").
        /// </summary>
        public string? Order
        {
            get => _order;
            set => _order = value?.Trim();
        }

        private string? _order;
    }
}
