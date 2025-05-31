using ExFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFit.DataAcces.Repository.IRepository
{
    public interface IUnitOfWork
    {
        //ICategoryRepository Category { get; }

            IWorkoutRepository Workout { get; }
            IMembershipPlanRepository MembershipPlan { get; }
        IRepository<UserMembership> UserMembership { get; }
        ICartRepository Cart { get; }
            ICartItemRepository CartItem { get; }
            void Save();
        

    }
}
