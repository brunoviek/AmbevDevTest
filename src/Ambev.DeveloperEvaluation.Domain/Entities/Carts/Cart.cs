using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Carts
{
    public class Cart
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
