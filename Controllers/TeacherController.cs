using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagement.Models;
using StudentManagement.Repositories;

namespace StudentManagement.Controllers;

[Authorize]
public class TeacherController : Controller
{
    private readonly ITeacherRepository _repository;
    private readonly ICourseRepository _courseRepository;

    public TeacherController(
        ITeacherRepository repository,
        ICourseRepository courseRepository)
    {
        _repository = repository;
        _courseRepository = courseRepository;
    }

    public IActionResult Index()
    {
        var teachers = _repository.GetAllTeachers();
        return View(teachers);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Courses = new SelectList(
            _courseRepository.GetAllCourses(),
            "CourseId",
            "CourseName");

        return View();
    }

    [HttpPost]
    public IActionResult Create(Teacher teacher)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Courses = new SelectList(
                _courseRepository.GetAllCourses(),
                "CourseId",
                "CourseName",
                teacher.CourseId);

            return View(teacher);
        }

        _repository.AddTeacher(teacher);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var teacher = _repository.GetTeacherById(id);

        if (teacher == null)
            return NotFound();

        ViewBag.Courses = new SelectList(
            _courseRepository.GetAllCourses(),
            "CourseId",
            "CourseName",
            teacher.CourseId);

        return View(teacher);
    }

    [HttpPost]
    public IActionResult Edit(Teacher teacher)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Courses = new SelectList(
                _courseRepository.GetAllCourses(),
                "CourseId",
                "CourseName",
                teacher.CourseId);

            return View(teacher);
        }

        _repository.UpdateTeacher(teacher);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Delete(int id)
    {
        _repository.DeleteTeacher(id);

        return RedirectToAction(nameof(Index));
    }
}