using Microsoft.AspNetCore.Mvc;
using PhalconSoft.Models;
using SimpleMvcSitemap;

namespace PhalconSoft.Controllers
{
    public class SiteMapController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly PhalconsoftContext _context;
        public SiteMapController(IConfiguration configuration,PhalconsoftContext context)
        {
            _configuration = configuration;
            _context = context;

        }
        public IActionResult Index()
        {
            //var domain = _configuration["Data:Domain"];
            //if (string.IsNullOrEmpty(domain))
            //{
            //    Console.WriteLine("domain boş");
            //    return Redirect("~/");
            //}
            List<SitemapNode> nodes = new List<SitemapNode>();


            ////  <-- Bloglar -->
            //foreach (var slug in _context.Categories.Where(i => i.IsApproved == true).Select(i => i.Slug).ToList())
            //{
            //    // add route name in sitemap
            //    nodes.Add(new SitemapNode($"{domain}/{slug}")
            //    {
            //        ChangeFrequency = ChangeFrequency.Weekly,
            //        LastModificationDate = DateTime.UtcNow.ToLocalTime(),
            //        Priority = 0.8M
            //    }
            //    );
            //    nodes.Add(new SitemapNode($"{domain}/{slug}/blog-detay")
            //    {
            //        ChangeFrequency = ChangeFrequency.Weekly,
            //        LastModificationDate = DateTime.UtcNow.ToLocalTime(),
            //        Priority = 0.8M
            //    }
            //    );
            //}


            nodes.Add(new SitemapNode("https://phalconsoft.com/")
            {
                ChangeFrequency = ChangeFrequency.Weekly,
                LastModificationDate = DateTime.UtcNow.ToLocalTime(),
                Priority = 1.0M
            });
            nodes.Add(new SitemapNode("https://phalconsoft.com/Portfolyo-Detay")
            {
                ChangeFrequency = ChangeFrequency.Weekly,
                LastModificationDate = DateTime.UtcNow.ToLocalTime(),
                Priority = 0.8M
            });
            nodes.Add(new SitemapNode("https://phalconsoft.com/Hakkimizda")
            {
                ChangeFrequency = ChangeFrequency.Weekly,
                LastModificationDate = DateTime.UtcNow.ToLocalTime(),
                Priority = 0.8M
            });
            nodes.Add(new SitemapNode("https://phalconsoft.com/Sanal-Pos/Odeme-Yap")
            {
                ChangeFrequency = ChangeFrequency.Weekly,
                LastModificationDate = DateTime.UtcNow.ToLocalTime(),
                Priority = 0.8M
            });
            nodes.Add(new SitemapNode("https://phalconsoft.com/Kurumsal")
            {
                ChangeFrequency = ChangeFrequency.Weekly,
                LastModificationDate = DateTime.UtcNow.ToLocalTime(),
                Priority = 0.8M
            });
            nodes.Add(new SitemapNode("https://phalconsoft.com/Fiyat-Teklifi")
            {
                ChangeFrequency = ChangeFrequency.Weekly,
                LastModificationDate = DateTime.UtcNow.ToLocalTime(),
                Priority = 0.64M
            });



            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
