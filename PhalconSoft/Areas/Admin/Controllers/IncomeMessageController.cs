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

    // GET: Admin/IncomeMessage/Delete/5
    // Silme onay sayfasını gösterir
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // Silinecek kaydı bul (await ile)
        var emailToDelete = await _context.SendEmails
            .FirstOrDefaultAsync(m => m.Id == id);

        if (emailToDelete == null)
        {
            return NotFound();
        }

        // Bulunan kaydı onay View'ına gönder
        return View(emailToDelete); // Delete.cshtml adında bir View olmalı
    }
// POST: Admin/IncomeMessage/Delete/5
    // Asıl silme işlemini yapar
    [HttpPost, ActionName("Delete")] // GET ile aynı URL'i kullanır ama POST isteği bekler
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) // Parametre adı id olmalı (formdan gelen gizli input ile eşleşir)
    {
        // Silinecek kaydı bul
        var emailToDelete = await _context.SendEmails.FindAsync(id);

        if (emailToDelete != null)
        {
            // Silmek için işaretle
            _context.SendEmails.Remove(emailToDelete);
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
        var sendEmail = await _context.SendEmails
            // .Include(s => s.RelatedUser) // Eğer ilişkili başka tablo varsa ve göstermek istersen
            .FirstOrDefaultAsync(m => m.Id == id); // ID'ye göre ilk kaydı getir

        // 3. Kayıt bulundu mu kontrol et
        if (sendEmail == null)
        {
            return NotFound(); // Kayıt yoksa bulunamadı hatası ver
        }

        // 4. Bulunan SendEmail nesnesini View'e model olarak gönder
        return View(sendEmail);
    }
}