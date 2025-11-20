using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PopQuiz.MVC.Models;

namespace PopQuiz.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public LoginController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7070/") 
            };

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                TempData["Error"] = "Debe ingresar el correo y la contraseña.";
                return View();
            }

            // ====== Construir el body ======
            var loginDto = new
            {
                email = email,
                password = password
            };

            var jsonBody = JsonSerializer.Serialize(loginDto);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // ====== Llamar API ======
            var response = await _httpClient.PostAsync("api/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Credenciales incorrectas o error en la API.";
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();

    

            var result = JsonSerializer.Deserialize<LoginResponse>(json, _jsonOptions);

            if (result == null)
            {
                TempData["Error"] = "No se pudo procesar la respuesta del servidor.";
                return View();
            }

            // ======== Guardar en sesión (ASP.NET Core) ========
            HttpContext.Session.SetInt32("UserID", result.UserID);
            HttpContext.Session.SetString("UserName", result.Name ?? "");
            HttpContext.Session.SetString("Role", result.Role ?? "");
            HttpContext.Session.SetString("Token", result.Token ?? "");


            return RedirectToAction("Index", "Home");
        }

        // GET: Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }


    public class LoginResponse
    {
        [JsonPropertyName("userId")]
        public int UserID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
