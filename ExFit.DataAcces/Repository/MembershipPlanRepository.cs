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
    public class MembershipPlanRepository : Repository<MembershipPlan>, IMembershipPlanRepository
    {
        private readonly ApplicationDbContext _db;

        public MembershipPlanRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MembershipPlan obj)
        {
            _db.MembershipPlans.Update(obj);
        }
    }
}
