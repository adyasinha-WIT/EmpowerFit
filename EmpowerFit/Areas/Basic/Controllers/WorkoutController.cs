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
    }
}
