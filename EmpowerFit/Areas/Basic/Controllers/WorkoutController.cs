using ExFit.DataAcces.Repository.IRepository;
using ExFit.Models;
using ExFit.DataAcces.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace EmpowerFit.Areas.Basic.Controllers
{
    [Area("Basic")]
    [Authorize(Roles = "Basic,Premium")]
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
            if (objWorkoutList == null || !objWorkoutList.Any())
            {
                TempData["error"] = "No workouts found!";
            }

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
        public IActionResult Upsert(Workout workout, IFormFile? mediaFile)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (mediaFile != null && mediaFile.Length > 0)
                {
                    // Validate file type and size (e.g., max 5MB)
                    var allowedTypes = new[] { ".jpg", ".jpeg", ".png", ".gif", ".mp4", ".webm" };
                    var ext = Path.GetExtension(mediaFile.FileName).ToLowerInvariant();
                    if (!allowedTypes.Contains(ext))
                    {
                        ModelState.AddModelError("", "Only image and video files are allowed.");
                        return View(workout);
                    }
                    if (mediaFile.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("", "File size must be under 5MB.");
                        return View(workout);
                    }

                    // Save file to wwwroot/uploads
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + ext;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        mediaFile.CopyTo(fileStream);
                    }

                    // Save relative path for web access
                    workout.MediaUrl = "/uploads/" + uniqueFileName;
                }
                else if (workout.Id != 0)
                {
                    // For update, preserve existing MediaUrl if no new file
                    var existing = _unitOfWork.Workout.Get(u => u.Id == workout.Id);
                    if (existing != null)
                        workout.MediaUrl = existing.MediaUrl;
                }

                if (workout.Id == 0)
                {
                    _unitOfWork.Workout.Add(workout);
                    TempData["success"] = "Workout created successfully";
                }
                else
                {
                    _unitOfWork.Workout.Update(workout);
                    TempData["success"] = "Workout updated successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(workout);
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
            var workouts = _unitOfWork.Workout.GetAll();
            var data = workouts.Select(w => new {
                goals = w.Goals,
                workouts = w.Workouts,
                type = w.Type,
                weeklyReport = w.WeeklyReport,
                mediaUrl = w.MediaUrl, // <-- Ensure this property exists and is set!
                id = w.Id
            }).ToList();

            return Json(new { data });
        }

        #endregion
    }
}

