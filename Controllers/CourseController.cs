using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Repositories;

namespace StudentManagement.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _repository;

        public CourseController(ICourseRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var courses = _repository.GetAllCourses();
            return View(courses);
        }

        public IActionResult Details(int id)
        {
            var course = _repository.GetCourseDetails(id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            if (!ModelState.IsValid)
                return View(course);

            _repository.AddCourse(course);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var course = _repository.GetCourseById(id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (!ModelState.IsValid)
                return View(course);

            _repository.UpdateCourse(course);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteCourse(id);
            return RedirectToAction(nameof(Index));
        }
    }
}