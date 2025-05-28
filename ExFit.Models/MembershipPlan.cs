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
        public string Name { get; set; } // e.g. "1 Month", "3 Months"
        public decimal Price { get; set; }
        public int DurationInDays { get; set; } // For logic to upgrade to premium
    }

}
