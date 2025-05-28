using ExFit.DataAcces.Data;
using ExFit.DataAcces.Repository.IRepository;
using ExFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFit.DataAcces.Repository
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        private readonly ApplicationDbContext _db;

        public CartItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CartItem obj)
        {
            _db.CartItems.Update(obj);
        }
    }
}
