using Microsoft.AspNetCore.Mvc;
using PhalconSoft.Models;

namespace PhalconSoft.Controllers;
[Route("[controller]/[action]")]

public class NewsletterController : Controller
{
    private readonly BlogContext _context;
    public NewsletterController(BlogContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public IActionResult Subscribe([FromForm] SubscribeViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Email) || !model.Email.Contains("@"))
        {
            return BadRequest(new { message = "Geçerli bir e-posta adresi girin." });
        }
        
        // TODO: Emaili veritabanına kaydet
        
        Console.WriteLine("Yeni abone: " + model.Email);
        return Json(new { message = "Abonelik başarıyla alındı." });

    }
}