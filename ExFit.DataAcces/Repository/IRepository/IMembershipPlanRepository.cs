using ExFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFit.DataAcces.Repository.IRepository
{
    public interface IMembershipPlanRepository : IRepository<MembershipPlan>
    {
        void Update(MembershipPlan obj);
    }
}
