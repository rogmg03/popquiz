using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PopQuiz.Architecture;
using PopQuiz.Architecture.Providers;
using PopQuiz.Data.Models;
using PopQuiz.MVCs.Models.ViewModels;


namespace PopQuiz.MVCs.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class MovieController : Controller
    {
        private readonly IRestProvider _restProvider;
        private readonly IConfiguration _configuration;
        private readonly string _apiBaseUrl;

        public MovieController(IRestProvider restProvider, IConfiguration configuration)
        {
            _restProvider = restProvider;
            _configuration = configuration;
            _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7136/api";
        }
        // GET: MovieController
        public async Task<IActionResult> Index()
        {
            try
            {
                var endpoint = $"{_apiBaseUrl}/MovieApi";
                var response = await _restProvider.GetAsync(endpoint, null);
                var movies = JsonProvider.DeserializeSimple<IEnumerable<MovieViewModels>>(response)
                           ?? new List<MovieViewModels>();

                return View(movies);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading movies: {ex.Message}";
                return View(new MovieViewModels());
            }
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModels movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var endpoint = $"{_apiBaseUrl}/MovieApi";
                    var json = JsonProvider.Serialize(movie);
                    
                    await _restProvider.PostAsync(endpoint, json);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating movie: {ex.Message}");
            }

            return View(movie);

        }

        

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieController/Edit/5
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

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieController/Delete/5
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
