using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopQuiz.Architecture;
using PopQuiz.Architecture.Providers;
using PopQuiz.Data.Models;
using PopQuiz.MVCs.Models.ViewModels;


namespace PopQuiz.MVCs.Controllers
{
    [Authorize(Roles = "Admin,User")]
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
        public async Task<IActionResult> Create(GenreViewModels genre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var endpoint = $"{_apiBaseUrl}/GenreApi";
                    var json = JsonProvider.Serialize(genre);

                    await _restProvider.PostAsync(endpoint, json);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating genre: {ex.Message}");
            }
            return View(genre);
        }

        // GET: GenreController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var endpoint = $"{_apiBaseUrl}/GenreApi/{id}";
                var response = await _restProvider.GetAsync(endpoint, id.ToString());
                var genre = JsonProvider.DeserializeSimple<GenreViewModels>(response);
                if (genre == null)
                    return NotFound();
                return View(genre);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading genre: {ex.Message}";
                return NotFound();
            }
        }

        // POST: GenreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GenreViewModels genre)
        {
            try
            {
                if (id != genre.GenreId)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    var endpoint = $"{_apiBaseUrl}/GenreApi/{id}";
                    var json = JsonProvider.Serialize(genre);
                    await _restProvider.PutAsync(endpoint, id.ToString(), json);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating genre: {ex.Message}");
            }
            return View(genre);
        }

        // GET: GenreController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var endpoint = $"{_apiBaseUrl}/GenreApi/{id}";
                var response = await _restProvider.GetAsync(endpoint, id.ToString());
                var genre = JsonProvider.DeserializeSimple<GenreViewModels>(response);
                if (genre == null)
                    return NotFound();
                return View(genre);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading genre: {ex.Message}";
                return NotFound();
            }
        }

        // POST: GenreController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var endpoint = $"{_apiBaseUrl}/GenreApi/{id}";
                await _restProvider.DeleteAsync(endpoint, id.ToString());
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error deleting genre: {ex.Message}";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
