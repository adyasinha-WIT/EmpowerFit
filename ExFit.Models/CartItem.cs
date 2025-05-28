using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFit.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int MembershipPlanId { get; set; }
        public MembershipPlan MembershipPlan { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }

}
