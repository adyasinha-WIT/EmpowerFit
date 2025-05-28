using ExFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFit.DataAcces.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        void Update(Cart obj);
    }
}
