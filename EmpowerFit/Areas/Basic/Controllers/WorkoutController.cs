using ExFit.DataAcces.Repository.IRepository;
using ExFit.Models;
using ExFit.DataAcces.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace EmpowerFit.Areas.Basic.Controllers
{
    [Area("Basic")]
    public class WorkoutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public WorkoutController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Workout> objWorkoutList = _unitOfWork.Workout.GetAll().ToList();

            return View(objWorkoutList);
        }
        public IActionResult Upsert(int? id)
        {

            Workout workout = new Workout();

            if (id == null || id == 0)
            {
                //create
                return View(workout);
            }
            else
            {
                //update
                workout = _unitOfWork.Workout.Get(u => u.Id == id);
                if (workout == null)
                {
                    return NotFound();
                }
                return View(workout);


            }
        }

        [HttpPost]
        public IActionResult Upsert( Workout workout)
        {
            if (ModelState.IsValid)
            {

                if (workout.Id == 0)
                {
                    _unitOfWork.Workout.Add(workout);
                    _unitOfWork.Save();
                    TempData["success"] = "Workout created successfully";
                }
                else
                {
                    _unitOfWork.Workout.Update(workout);
                    _unitOfWork.Save();
                    TempData["success"] = "Workout updated successfully";
                }
               
                return RedirectToAction("Index");

            }


            else
            {
               
                return View(workout);
            }
        }




        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var workoutToBeDeleted = _unitOfWork.Workout.Get(u => u.Id == id);
            if (workoutToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });

            }
           
            _unitOfWork.Workout.Remove(workoutToBeDeleted);
            _unitOfWork.Save();
            return Json(new { sucess = true, message = "Delete Successful" });
        }
        #region API calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Workout> objWorkoutList = _unitOfWork.Workout.GetAll().ToList();
            return Json(new { data = objWorkoutList });
        }
        #endregion
    }
}

