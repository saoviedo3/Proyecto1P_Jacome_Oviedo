using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Entities;

namespace WebApp.SecurityGUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductsController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri("http://localhost:56104/api/") // Cambia el puerto si es necesario.
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Mostrar lista de productos
        [Authorize(Roles = "View, Admin")]
        public async Task<ActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("Products");
                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadAsAsync<List<Products>>();
                    return View(products);
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Error al conectar con la API: {ex.Message}");
            }

            return View(new List<Products>()); // Retorna una lista vacía si falla.
        }

        // Crear producto
        [Authorize(Roles = "Admin, create")]
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Products());
        }

        [Authorize(Roles = "Admin, create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Products product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync("Products", product);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Producto creado correctamente.";
                        return RedirectToAction("Index");
                    }
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError("", $"Error al conectar con la API: {ex.Message}");
                }
                ModelState.AddModelError("", "Error al crear el producto.");
            }
            return View(product);
        }

        // Editar producto
        [Authorize(Roles = "Edit, Admin")]
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var product = await response.Content.ReadAsAsync<Products>();
                    return View(product);
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Error al conectar con la API: {ex.Message}");
            }

            TempData["Error"] = "Error al cargar el producto.";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Edit, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Products product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync($"Products/{product.ProductID}", product);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Producto actualizado correctamente.";
                        return RedirectToAction("Index");
                    }
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError("", $"Error al conectar con la API: {ex.Message}");
                }
                ModelState.AddModelError("", "Error al actualizar el producto.");
            }

            return View(product);
        }

        // Eliminar producto
        [Authorize(Roles = "Delete, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Producto eliminado correctamente.";
                    return RedirectToAction("Index");
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Error al conectar con la API: {ex.Message}");
            }

            TempData["Error"] = "Error al eliminar el producto.";
            return RedirectToAction("Index");
        }
    }
}
