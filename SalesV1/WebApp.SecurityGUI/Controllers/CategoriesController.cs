using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Entities;

namespace WebApp.SecurityGUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoriesController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri("http://localhost:56104/api/") // Cambia el puerto si es necesario.
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Authorize(Roles = "View, Admin")]
        public async Task<ActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("Categories");
                if (response.IsSuccessStatusCode)
                {
                    var categories = await response.Content.ReadAsAsync<List<Categories>>();
                    return View(categories);
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Error al conectar con la API: {ex.Message}");
            }

            return View(new List<Categories>());
        }

        [Authorize(Roles = "Admin, create")]
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Categories());
        }

        [Authorize(Roles = "Admin, create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Categories category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync("Categories", category);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Categoría creada correctamente.";
                        return RedirectToAction("Index");
                    }
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError("", $"Error al conectar con la API: {ex.Message}");
                }
                ModelState.AddModelError("", "Error al crear la categoría.");
            }
            return View(category);
        }

        // Editar categoría
        [Authorize(Roles = "Edit, Admin")]
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Categories/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var category = await response.Content.ReadAsAsync<Categories>();
                    return View(category);
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Error al conectar con la API: {ex.Message}");
            }

            TempData["Error"] = "Error al cargar la categoría.";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Edit, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Categories category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync($"Categories/{category.CategoryID}", category);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Categoría actualizada correctamente.";
                        return RedirectToAction("Index");
                    }
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError("", $"Error al conectar con la API: {ex.Message}");
                }
                ModelState.AddModelError("", "Error al actualizar la categoría.");
            }

            return View(category);
        }

        // Eliminar categoría
        [Authorize(Roles = "Delete, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Categories/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Categoría eliminada correctamente.";
                    return RedirectToAction("Index");
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Error al conectar con la API: {ex.Message}");
            }

            TempData["Error"] = "Error al eliminar la categoría.";
            return RedirectToAction("Index");
        }
    }
}
