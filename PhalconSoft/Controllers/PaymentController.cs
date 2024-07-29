using Microsoft.AspNetCore.Mvc;

namespace PhalconSoft.Controllers
{
    [Route("Sanal-Pos")]
    public class PaymentController : Controller
    {
     
        [HttpGet,Route("Odeme-Yap")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
