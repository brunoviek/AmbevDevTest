using Ambev.DeveloperEvaluation.Application.Carts.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    /// <summary>
    /// Query to retrieve a cart by its identifier.
    /// </summary>
    public class GetCartQuery : IRequest<CartResult>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GetCartQuery"/>.
        /// </summary>
        public GetCartQuery(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the identifier of the cart to retrieve.
        /// </summary>
        public int Id { get; }
    }
}
