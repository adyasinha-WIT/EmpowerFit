using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFit.Models
{
    public class Workout
    {
        public int Id { get; set; }
        [StringLength(500, ErrorMessage = "Goals cannot exceed 500 characters.")]
        public string Goals { get; set; }
        [StringLength(500, ErrorMessage = "Goals cannot exceed 500 characters.")]
        public string Workouts { get; set; }
        [StringLength(500, ErrorMessage = "Goals cannot exceed 500 characters.")]

        public string Type { get; set; }
        [StringLength(500, ErrorMessage = "Goals cannot exceed 500 characters.")]

        public string WeeklyReport { get; set; }

        [Url(ErrorMessage = "Media URL must be a valid URL.")]
        public string? MediaUrl { get; set; } // For image/video path
    }
}
