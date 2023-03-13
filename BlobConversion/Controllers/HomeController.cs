using BlobConversion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlobConversion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            byte[] blobArray = System.IO.File.ReadAllBytes("BlobData/Blob.pdf");
            string base64String = $"data:application/pdf;base64,{Convert.ToBase64String(blobArray)}";
            var model = new Blob(base64String, blobArray.Length);

            return View(model);
        }

        [ResponseCache(Duration = 300, VaryByQueryKeys = new[] { "*" })]
        public FileContentResult PdfBlob()
        {
            var pdfBlob = System.IO.File.ReadAllBytes("BlobData/Blob.pdf");
            return new FileContentResult(pdfBlob, "application/pdf");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string ConvertToBase64(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return Convert.ToBase64String(bytes);
        }
    }
}