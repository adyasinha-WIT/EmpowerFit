﻿using ExFit.Models;
using System.Net.Http.Headers;

namespace ExFit.DataAcces.Repository.IRepository
{
    public interface IWorkoutRepository:IRepository<Workout>
    {
        void Update(Workout obj);
        void Save();
    }

}
