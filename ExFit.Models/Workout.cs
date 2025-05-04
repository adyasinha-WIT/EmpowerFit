using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFit.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Goals { get; set; }
        public string Workouts { get; set; }

        public string Type { get; set; }

        public string WeeklyReport { get; set; }
    }
}
