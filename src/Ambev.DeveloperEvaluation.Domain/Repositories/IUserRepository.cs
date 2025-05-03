using Ambev.DeveloperEvaluation.Domain.Entities.Users;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Users entity operations
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Creates a new user in the repository
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the base users query as an <see cref="IQueryable{User}"/>,
    /// with change tracking disabled (<c>AsNoTracking</c>),
    /// allowing filters, sorting, and pagination to be composed before execution.
    /// </summary>
    /// <returns>
    /// An <see cref="IQueryable{User}"/> representing all users in the database,
    /// ready for further query composition and eventual execution via EF Core.
    /// </returns>
    IQueryable<User> QueryAll();

    /// <summary>
    /// Updates an existing user in the data store.
    /// </summary>
    /// <param name="user">The user entity with updated data.</param>
    Task UpdateAsync(User user);
}
