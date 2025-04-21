using Microsoft.AspNetCore.Mvc;
using PhalconSoft.Models;
using System.Diagnostics;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.Extensions.Primitives;

namespace PhalconSoft.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;


        public HomeController(BlogContext context,ILogger<HomeController> logger,INotyfService notyf)
        {
            _context = context;
            _logger = logger;
            _notyf = notyf;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmail(SendEmailViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var sendEmail = new SendEmail()
            {
                Email = model.Email,
                NameSurname = model.NameSurname,
                Subject = model.Subject,
                Message = model.Message
            };
            // _context.SendEmails.Add(model);
            _context.SaveChanges();
            _notyf.Success("Success Notification");


            
            return Ok("İşlem Başarılı.");
        }
        
        
    }
}