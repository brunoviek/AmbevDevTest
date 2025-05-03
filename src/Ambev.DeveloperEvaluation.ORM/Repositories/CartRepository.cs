using Ambev.DeveloperEvaluation.Common.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// EF Core implementation of <see cref="ICartRepository"/>.
    /// </summary>
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of <see cref="CartRepository"/>.
        /// </summary>
        public CartRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<Cart> AddAsync(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        /// <inheritdoc />
        public async Task<Cart> GetByIdAsync(int id)
        {
            var cart = await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == id);

            if (cart is null)
                throw new BadRequestException($"Cart with Id {id} not found.");

            return cart;
        }

        /// <inheritdoc />
        public async Task<Cart> UpdateAsync(Cart cart)
        {
            var existing = await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == cart.Id);

            if (existing == null)
                throw new BadRequestException($"Cart with Id {cart.Id} not found.");

            existing.UserId = cart.UserId;
            existing.Date = cart.Date;

            _context.CartItems.RemoveRange(existing.Items);
            existing.Items = cart.Items;

            await _context.SaveChangesAsync();
            return existing;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
                throw new BadRequestException($"Cart with Id {id} not found.");

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public IQueryable<Cart> QueryAll()
        {
            return _context.Carts
                           .AsNoTracking()
                           .Include(c => c.Items);
        }
    }
}
