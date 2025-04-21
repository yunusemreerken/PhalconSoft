using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhalconSoft.Models;

namespace PhalconSoft.Areas.Admin.Controllers;
[Area("Admin")]
//[Authorize]
public class IncomeMessageController : Controller
{
    private readonly BlogContext _context;

    public IncomeMessageController(BlogContext context)
    {
        _context = context;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {

        var emails = await _context.SendEmails.ToListAsync();
        return View(emails);
    }
}