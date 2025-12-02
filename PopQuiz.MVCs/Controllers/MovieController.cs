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
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var endpoint = $"{_apiBaseUrl}/MovieApi/{id}";
                var response = await _restProvider.GetAsync(endpoint, id.ToString());
                var movie = JsonProvider.DeserializeSimple<MovieViewModels>(response);
                if (movie == null)
                    return NotFound();
                return View(movie);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading movie: {ex.Message}";
                return NotFound();
            }
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieViewModels movie)
        {
            try
            {
                if (id != movie.MovieId)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    var endpoint = $"{_apiBaseUrl}/MovieApi/{id}";
                    var json = JsonProvider.Serialize(movie);
                    await _restProvider.PutAsync(endpoint, id.ToString(), json);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating movie: {ex.Message}");
            }
            return View(movie);
        }

        // GET: MovieController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var endpoint = $"{_apiBaseUrl}/MovieApi/{id}";
                var response = await _restProvider.GetAsync(endpoint, id.ToString());
                var movie = JsonProvider.DeserializeSimple<MovieViewModels>(response);
                if (movie == null)
                    return NotFound();
                return View(movie);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading movie: {ex.Message}";
                return NotFound();
            }
        }

        // POST: MovieController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var endpoint = $"{_apiBaseUrl}/MovieApi/{id}";
                await _restProvider.DeleteAsync(endpoint, id.ToString());
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error deleting movie: {ex.Message}";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
