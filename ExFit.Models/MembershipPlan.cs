using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFit.Models
{
    public class MembershipPlan
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public decimal Price { get; set; }
        public int DurationInDays { get; set; } // logic to upgrade to premium
    }

}
