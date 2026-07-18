using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Repositories;

namespace StudentManagement.Controllers;

[Authorize]
public class StudentController : Controller
{
    private readonly IStudentRepository _repository;

    public StudentController(IStudentRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index(string? searchTerm)
    {
        IEnumerable<Student> students;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            students = _repository.GetAllStudents();
        }
        else
        {
            students = _repository.SearchStudents(searchTerm);
        }

        ViewBag.SearchTerm = searchTerm;

        return View(students);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Student student)
    {
        if (!ModelState.IsValid)
            return View(student);

        _repository.AddStudent(student);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var student = _repository.GetStudentById(id);

        if (student == null)
            return NotFound();

        return View(student);
    }

    [HttpPost]
    public IActionResult Edit(Student student)
    {
        if (!ModelState.IsValid)
            return View(student);

        _repository.UpdateStudent(student);

        return RedirectToAction(nameof(Index));
    }

   [Authorize(Roles = "Admin")]
[HttpPost]
public IActionResult Delete(int id)
{
    _repository.DeleteStudent(id);

    return RedirectToAction(nameof(Index));
}
}