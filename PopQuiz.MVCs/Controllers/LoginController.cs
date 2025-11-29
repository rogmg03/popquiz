using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PopQuiz.Architecture;
using PopQuiz.Architecture.Providers;
using PopQuiz.MVCs.Models;
using PopQuiz.MVCs.Models.ViewModels;
using PopQuiz.Web.Models.ViewModels;
using System.Security.Cryptography;

namespace PopQuiz.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRestProvider _restProvider;
        private readonly IConfiguration _configuration;
        private readonly string _apiBaseUrl;

        public LoginController(IRestProvider restProvider, IConfiguration configuration)
        {
            _restProvider = restProvider;
            _configuration = configuration;
            _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
        }

        [HttpGet]
        public IActionResult Index()
        {
            // If already logged in, redirect to home
            if (IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            // Check for success message from registration
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Get all users from API
                var endpoint = $"{_apiBaseUrl}/UserApi";
                var response = await _restProvider.GetAsync(endpoint, null);
                var users = JsonProvider.DeserializeSimple<List<UserViewModels>>(response) ?? new List<UserViewModels>();

                // Find user by username or email
                var user = users.FirstOrDefault(u =>
                    (u.Email != null && u.Email.Equals(model.email, StringComparison.OrdinalIgnoreCase)));

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }


                // Verify password
                if (!VerifyPassword(model.Password, user.Password))
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }

                // Store user in session
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("Username", user.Name ?? "");
                HttpContext.Session.SetString("Email", user.Email ?? "");
                HttpContext.Session.SetString("IsLoggedIn", "true");

                // Store login in database (optional - you can create a UserLogin table)
                // For now, we'll just store in session

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error during login: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            // If already logged in, redirect to home
            if (IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Check if username already exists
                var endpoint = $"{_apiBaseUrl}/UserApi";
                var response = await _restProvider.GetAsync(endpoint, null);
                var users = JsonProvider.DeserializeSimple<List<UserViewModels>>(response) ?? new List<UserViewModels>();

                if (users.Any(u => u.Name != null && u.Name.Equals(model.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("Username", "Username is already taken.");
                    return View(model);
                }

                // Check if email already exists
                if (users.Any(u => u.Email != null && u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                    return View(model);
                }

                // Hash the password
                var passwordHash = HashPassword(model.Password);

                // Create new user
                var newUser = new UserViewModels
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = passwordHash
                };

                // Save user via API
                var json = JsonProvider.Serialize(newUser);
                await _restProvider.PostAsync(endpoint, json);

                // Redirect to login with success message
                TempData["SuccessMessage"] = "Registration successful! Please login with your credentials.";
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error during registration: {ex.Message}");
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetString("IsLoggedIn") == "true";
        }

        private string HashPassword(string password)
        {
            // Hash password using SHA256 (same method as verification)
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string? passwordHash)
        {
            if (string.IsNullOrEmpty(passwordHash))
            {
                return false;
            }

            // Simple password verification - hash the input password and compare
            // For production, use proper password hashing like BCrypt or ASP.NET Identity's PasswordHasher
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedPassword = Convert.ToBase64String(hashedBytes);
                return hashedPassword == passwordHash;
            }
        }

    }

}
