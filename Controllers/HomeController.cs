using ImageCompressor.Models;
using ImageCompressor.Services;
using ImageCompressor.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace ImageCompressor.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlobStorage _blobStorageService;
        public HomeController(IBlobStorage blobStorageService)
        {
            _blobStorageService= blobStorageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Upload(UploadImageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.ImageFile == null || model.ImageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Please select a file.");
                return View(model);
            }

            string imageUrl = await _blobStorageService.UploadImageAsync(model.ImageFile);
            // You can handle the response here, such as displaying the uploaded image URL.
            return RedirectToAction("Index", "Home");
        }
    }
}