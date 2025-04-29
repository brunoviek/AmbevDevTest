using Ambev.DeveloperEvaluation.Domain.Entities.User;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of UserRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetAllAsync(int page, int size, string? order, CancellationToken cancellationToken = default)
    {
        var UsersQuery = _context.Users.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(order))
        {
            UsersQuery = UsersQuery.OrderBy(ValidateOrderFields(order));
        }
        else
        {
            UsersQuery = UsersQuery.OrderBy(u => u.Username);
        }

        var users = await UsersQuery
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return users;
    }

    private static readonly HashSet<string> AllowedOrderFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "Username",
        "Email",
        "Phone",
        "Status",
        "Role",
        "CreatedAt",
        "FirstName",
        "LastName",
        "City",
        "Street",
        "Zipcode"
    };

    private static string ValidateOrderFields(string order)
    {
        var fields = order.Split(',');

        foreach (var field in fields)
        {
            var fieldName = field.Trim().Split(' ')[0];
            if (!AllowedOrderFields.Contains(fieldName))
            {
                throw new ArgumentException($"Invalid ordering field: {fieldName}");
            }
        }

        return order;
    }
}
