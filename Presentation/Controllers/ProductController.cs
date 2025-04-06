using DataTransferObjects.ProductDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Models;
using Presentation.ViewBags;
using Presentation.ViewBags.ProductViewBag;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7240/api/products");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<MainProductViewBag>>(jsonData);

                return View(products);
            }

            return View("Error", new ErrorViewBag
            {
                StatusCode = (int)responseMessage.StatusCode,
                Message = "Veriler alınırken bir hata oluştu."
            });
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(AddProductViewBag addProductViewBag)
        {
            if (!ModelState.IsValid)
                return View(addProductViewBag);

            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(addProductViewBag);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7240/api/products", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.Message = "Ürün başarıyla eklendi.";
                return RedirectToAction("Index");
            }

            ViewBag.Error = $"Hata Kodu: {(int)responseMessage.StatusCode} - Ürün kaydedilemedi.";
            return View(addProductViewBag);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7240/api/products/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<UpdateProductViewBag>(jsonData);
                return View(product);
            }
            return View("Error", new ErrorViewBag
            {
                StatusCode = (int)responseMessage.StatusCode,
                Message = "Ürün bulunamadı."
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductViewBag updateProductViewBag)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductViewBag);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("https://localhost:7240/api/products", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7240/api/products/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Ürün başarıyla silindi.";
            }
            else
            {
                TempData["Error"] = "Silme işlemi başarısız.";
            }

            return RedirectToAction("Index");
        }

    }
}
