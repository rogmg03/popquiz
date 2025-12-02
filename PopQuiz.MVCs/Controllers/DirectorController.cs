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
    public class DirectorController : Controller
    {
        private readonly IRestProvider _restProvider;
        private readonly IConfiguration _configuration;
        private readonly string _apiBaseUrl;
        public DirectorController(IRestProvider restProvider, IConfiguration configuration)
        {
            _restProvider = restProvider;
            _configuration = configuration;
            _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7136/api";
        }
        // GET: DirectorController
        public async Task<IActionResult> Index()
        {
            try
            {
                var endpoint = $"{_apiBaseUrl}/DirectorApi";
                var response = await _restProvider.GetAsync(endpoint, null);
                var director = JsonProvider.DeserializeSimple<IEnumerable<DirectorViewModels>>(response)
                           ?? new List<DirectorViewModels>();

                return View(director);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading categories: {ex.Message}";
                return View(new DirectorViewModels());
            }
        }

        // GET: DirectorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DirectorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DirectorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DirectorViewModels director)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var endpoint = $"{_apiBaseUrl}/DirectorApi";
                    var json = JsonProvider.Serialize(director);

                    await _restProvider.PostAsync(endpoint, json);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating director: {ex.Message}");
            }

            return View(director);
        }

        // GET: DirectorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var endpoint = $"{_apiBaseUrl}/DirectorApi/{id}";
                var response = await _restProvider.GetAsync(endpoint, id.ToString());
                var director = JsonProvider.DeserializeSimple<DirectorViewModels>(response);
                if (director == null)
                    return NotFound();
                return View(director);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading director: {ex.Message}";
                return NotFound();
            }
        }

        // POST: DirectorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DirectorViewModels director)
        {
            try
            {
                if (id != director.DirectorId)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    var endpoint = $"{_apiBaseUrl}/DirectorApi/{id}";
                    var json = JsonProvider.Serialize(director);
                    await _restProvider.PutAsync(endpoint, id.ToString(), json);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating director: {ex.Message}");
            }
            return View(director);
        }

        // GET: DirectorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var endpoint = $"{_apiBaseUrl}/DirectorApi/{id}";
                var response = await _restProvider.GetAsync(endpoint, id.ToString());
                var director = JsonProvider.DeserializeSimple<DirectorViewModels>(response);
                if (director == null)
                    return NotFound();
                return View(director);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading director: {ex.Message}";
                return NotFound();
            }
        }

        // POST: DirectorController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var endpoint = $"{_apiBaseUrl}/DirectorApi/{id}";
                await _restProvider.DeleteAsync(endpoint, id.ToString());
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error deleting director: {ex.Message}";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
