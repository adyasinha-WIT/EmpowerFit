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
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public IMembershipPlanRepository MembershipPlan { get; private set; }
        public IRepository<UserMembership> UserMembership { get; }
        public ICartRepository Cart { get; private set; }
        public ICartItemRepository CartItem { get; private set; }
        public IWorkoutRepository Workout { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
          
            Workout = new WorkoutRepository(_db);
            MembershipPlan = new MembershipPlanRepository(_db);
            Cart = new CartRepository(_db);
            CartItem = new CartItemRepository(_db);
            UserMembership = new Repository<UserMembership>(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
