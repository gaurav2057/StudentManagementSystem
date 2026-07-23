using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Repositories;

namespace StudentManagement.Controllers;

public class HomeController : Controller
{
    private readonly IStudentRepository _repository;

    public HomeController(IStudentRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var dashboard = _repository.GetDashboardData();
        return View(dashboard);
    }

    // NEW ACTION
    public IActionResult About()
    {
        return View();
    }

    // NEW ACTION
    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}