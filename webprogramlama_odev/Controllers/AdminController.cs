using Microsoft.AspNetCore.Mvc;
using webprogramlama_odev.Data;
using webprogramlama_odev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace webprogramlama_odev.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }

        // Admin Paneli Ana Sayfası
        public IActionResult Admin()
        {
            return View();
        }

        
    }
}
