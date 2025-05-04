using ExFit.DataAcces.Data;
using ExFit.DataAcces.Repository.IRepository;
using ExFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFit.DataAcces.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        //public ICategoryRepository Category { get; private set; }
        public IWorkoutRepository Workout { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            using var _db = db;
          
            Workout = new WorkoutRepository(_db);
        }



        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
