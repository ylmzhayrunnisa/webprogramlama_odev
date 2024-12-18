using webprogramlama_odev.Data;
using Microsoft.AspNetCore.Mvc;

namespace webprogramlama_odev.Controllers
{
    public class AnasayfaController : Controller
    {
        public IActionResult Anasayfa()
        {
            return View();
        }
    }
}
