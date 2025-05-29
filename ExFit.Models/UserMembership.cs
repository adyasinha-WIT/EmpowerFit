using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFit.Models
{
    public class UserMembership
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PlanId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}

