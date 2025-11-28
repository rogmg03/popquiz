using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopQuiz.Architecture;
using PopQuiz.Architecture.Providers;
using PopQuiz.MVC.Models.ViewModels;
using PopQuiz.Web.Filters;

namespace PopQuiz.MVC.Controllers
{

    public class GenreController : Controller
    {
        private readonly IRestProvider _restProvider;
        private readonly IConfiguration _configuration;
        private readonly string _apiBaseUrl;
        public GenreController(IRestProvider restProvider, IConfiguration configuration)
        {
            _restProvider = restProvider;
            _configuration = configuration;
            _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7136/api";
        }
        // GET: GenreController
        public async Task<IActionResult> Index()
        {
            try
            {
                var endpoint = $"{_apiBaseUrl}/GenreApi";
                var response = await _restProvider.GetAsync(endpoint, null);
                var genre = JsonProvider.DeserializeSimple<IEnumerable<GenreViewModels>>(response)
                           ?? new List<GenreViewModels>();

                return View(genre);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading categories: {ex.Message}";
                return View(new GenreViewModels());
            }
        }

        // GET: GenreController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GenreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GenreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GenreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
