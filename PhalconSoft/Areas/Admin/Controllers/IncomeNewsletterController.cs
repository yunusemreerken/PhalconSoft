using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhalconSoft.Models;

namespace PhalconSoft.Areas.Admin.Controllers;
[Area("Admin")]

public class IncomeNewsletterController : Controller
{
    private readonly BlogContext _context;

    public IncomeNewsletterController(BlogContext context)
    {
        _context = context;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {

        var newsletter = await _context.NewsletterSubscribers.ToListAsync();
        return View(newsletter);
    }

    // GET: Admin/IncomeMessage/Delete/5
    // Silme onay sayfasını gösterir
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // Silinecek kaydı bul (await ile)
        var newsletterToDelete = await _context.NewsletterSubscribers
            .FirstOrDefaultAsync(m => m.Id == id);

        if (newsletterToDelete == null)
        {
            return NotFound();
        }

        // Bulunan kaydı onay View'ına gönder
        return View(newsletterToDelete); // Delete.cshtml adında bir View olmalı
    }
// POST: Admin/IncomeMessage/Delete/5
    // Asıl silme işlemini yapar
    [HttpPost, ActionName("Delete")] // GET ile aynı URL'i kullanır ama POST isteği bekler
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) // Parametre adı id olmalı (formdan gelen gizli input ile eşleşir)
    {
        // Silinecek kaydı bul
        var newsletterToDelete = await _context.NewsletterSubscribers.FindAsync(id);

        if (newsletterToDelete != null)
        {
            // Silmek için işaretle
            // TODO:çözülecek
            //_context.SendEmails.Remove(newsletterToDelete);
            // Değişiklikleri kaydet (await ile)
            await _context.SaveChangesAsync();
        }
        // else durumunda belki bir hata mesajı gösterilebilir veya loglanabilir

        // Index sayfasına yönlendir
        return RedirectToAction(nameof(Index));
    }
    // GET: Admin/IncomeMessage/Details/5
    // Belirtilen ID'ye sahip e-postanın detaylarını gösterir
    public async Task<IActionResult> Details(int? id) // ID nullable olmalı
    {
        // 1. Gelen ID null mı kontrol et
        if (id == null)
        {
            return NotFound(); // ID yoksa bulunamadı hatası ver
        }

        // 2. Veritabanından ilgili kaydı ID'ye göre bul
        // Include ile ilişkili başka verileri çekmek gerekmiyorsa FindAsync daha performanslı olabilir
        var newsletterEmail = await _context.NewsletterSubscribers
            // .Include(s => s.RelatedUser) // Eğer ilişkili başka tablo varsa ve göstermek istersen
            .FirstOrDefaultAsync(m => m.Id == id); // ID'ye göre ilk kaydı getir

        // 3. Kayıt bulundu mu kontrol et
        if (newsletterEmail == null)
        {
            return NotFound(); // Kayıt yoksa bulunamadı hatası ver
        }

        // 4. Bulunan SendEmail nesnesini View'e model olarak gönder
        return View(newsletterEmail);
    }
}