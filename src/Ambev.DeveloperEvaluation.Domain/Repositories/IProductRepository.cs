using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Product entity operations
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Checks if a product with the specified title exists.
        /// </summary>
        /// <param name="title">The product title to check for uniqueness.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if a product exists with the given title; otherwise, false.</returns>
        Task<bool> ExistsByTitleAsync(string title, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">The product entity to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The added product entity.</returns>
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The product entity if found; otherwise, null.</returns>
        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="product">The product entity with updated values.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task UpdateAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the base Product query as an <see cref="IQueryable{Product}"/>,
        /// with change tracking disabled (<c>AsNoTracking</c>),
        /// allowing filters, sorting, and pagination to be composed before execution.
        /// </summary>
        /// <returns>
        /// An <see cref="IQueryable{Product}"/> representing all Product in the database,
        /// ready for further query composition and eventual execution via EF Core.
        /// </returns>
        IQueryable<Product> QueryAll();
    }
}
