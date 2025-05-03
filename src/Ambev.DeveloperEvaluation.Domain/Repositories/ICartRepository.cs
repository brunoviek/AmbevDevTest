using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Provides CRUD operations for carts and their items.
    /// </summary>
    public interface ICartRepository
    {
        /// <summary>
        /// Adds a new cart along with its items.
        /// </summary>
        Task<Cart> AddAsync(Cart cart);

        /// <summary>
        /// Retrieves a cart by its identifier.
        /// </summary>
        Task<Cart> GetByIdAsync(int id);

        /// <summary>
        /// Updates an existing cart and its items.
        /// </summary>
        Task<Cart> UpdateAsync(Cart cart);

        /// <summary>
        /// Deletes a cart by its identifier.
        /// </summary>
        Task DeleteAsync(int id);

        /// <summary>
        /// Returns an <see cref="IQueryable{Cart}"/> of all carts and their items,
        /// configured for read-only access (no change tracking).
        /// </summary>
        IQueryable<Cart> QueryAll();
    }
}
