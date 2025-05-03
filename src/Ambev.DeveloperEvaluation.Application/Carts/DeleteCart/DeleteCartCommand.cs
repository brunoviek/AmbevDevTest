using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    /// <summary>
    /// Command to delete a cart by its identifier.
    /// </summary>
    public class DeleteCartCommand : IRequest<string>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DeleteCartCommand"/>.
        /// </summary>
        public DeleteCartCommand(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the identifier of the cart to delete.
        /// </summary>
        public int Id { get; }
    }
}