using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExFit.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Membership Plan is required.")]
        public int MembershipPlanId { get; set; }
        public MembershipPlan MembershipPlan { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }

}
