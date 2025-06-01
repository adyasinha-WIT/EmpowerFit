using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFit.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }

}
