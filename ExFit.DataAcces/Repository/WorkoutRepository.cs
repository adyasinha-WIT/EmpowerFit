using ExFit.DataAcces.Data;
using ExFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExFit.Models;
using ExFit.DataAcces.Repository.IRepository;
using System.Net.Http.Headers;

namespace ExFit.DataAcces.Repository
{
    public class WorkoutRepository: Repository<Workout>, IWorkoutRepository
    {
        private ApplicationDbContext _db;
        public WorkoutRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Workout obj)
        {
            var objFromDb = _db.Workouts.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Goals = obj.Goals;
                objFromDb.Workouts = obj.Workouts;
                objFromDb.Type = obj.Type;
                objFromDb.WeeklyReport = obj.WeeklyReport;
          
               

            }
        }

        public void Update(ProductHeaderValue obj)
        {
            throw new NotImplementedException();
        }
    }
}