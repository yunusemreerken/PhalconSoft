using Microsoft.AspNetCore.Mvc;
using PhalconSoft.Models;
using System.Diagnostics;

namespace PhalconSoft.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PhalconsoftContext _context;

        public HomeController(ILogger<HomeController> logger, PhalconsoftContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet,Route("Portfolyo-Detay")]
        public IActionResult PortfolioDetail()
        {
            return View();
        }
        [HttpGet,Route("Fiyat-Teklifi")]
        public IActionResult PriceOffer()
        {
            return View();
        }
        [HttpGet, Route("Kurumsal")]
        public IActionResult Corporate()
        {
            return View();
        }
        [HttpGet,Route("Hakkimizda")]
        public IActionResult AboutUs()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}