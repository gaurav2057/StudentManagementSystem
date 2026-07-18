using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Repositories;

namespace StudentManagement.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher<AppUser> _passwordHasher;

    public AccountController(
        IUserRepository repository,
        IPasswordHasher<AppUser> passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    // ---------------- Register ----------------

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

[HttpPost]
public IActionResult Register(RegisterViewModel model)
{
    if (!ModelState.IsValid)
        return View(model);

    model.Email = model.Email.Trim().ToLower();

    var existingUser = _repository.GetByEmail(model.Email);

    if (existingUser != null)
    {
        ModelState.AddModelError(nameof(model.Email),
            "This email address is already registered.");

        return View(model);
    }

    var user = new AppUser
    {
        FullName = model.FullName,
        Email = model.Email,
        Role = "User"
    };

    user.PasswordHash =
        _passwordHasher.HashPassword(user, model.Password);

    _repository.Register(user);

    TempData["Success"] = "Registration successful. Please login.";

    return RedirectToAction(nameof(Login));
}
    // ---------------- Login ----------------

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _repository.GetByEmail(model.Email);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid Email or Password");
            return View(model);
        }

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            model.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError("", "Invalid Email or Password");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        return RedirectToAction("Index", "Home");
    }

    // ---------------- Logout ----------------

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login");
    }
}