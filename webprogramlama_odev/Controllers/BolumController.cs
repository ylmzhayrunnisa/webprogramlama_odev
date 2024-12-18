using Microsoft.AspNetCore.Mvc;
using webprogramlama_odev.Models;
using System.Linq;
using webprogramlama_odev.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace webprogramlama_odev.Controllers
{
    public class BolumController : Controller
    {
        private readonly DatabaseContext _context;

        public BolumController(DatabaseContext context)
        {
            _context = context;

            if (!_context.Bolumler.Any())
            {
                _context.Bolumler.Add(new Bolum() { BolumAdi = "Çocuk Acil", HastaSayisi = 254, BosYatakSayisi = 8 });
                _context.Bolumler.Add(new Bolum() { BolumAdi = "Çocuk Yoğun Bakımı", HastaSayisi = 49, BosYatakSayisi = 15 });
                _context.Bolumler.Add(new Bolum() { BolumAdi = "Çocuk Hematolojisi ve Onkolojisi", HastaSayisi = 214, BosYatakSayisi = 24 });

                _context.SaveChanges();
            }
        }

        public IActionResult Bolum()
        {
            var bolumler = _context.Bolumler.ToList();
            return View(bolumler);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult BolumList()
        {
            var bolumler = _context.Bolumler.ToList();
            return View(bolumler);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult BolumAdd(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult BolumAdd(Bolum bolum)
        {
            // Eğer ModelState geçerli değilse, formu tekrar gösteriyoruz
            if (!ModelState.IsValid)
            {
                // Bölüm listesini ViewBag ile tekrar göndermek
                ViewBag.Bolumler = _context.Bolumler.ToList();
                return View(bolum);
            }

            _context.Bolumler.Add(bolum);
            _context.SaveChanges();
            return RedirectToAction("BolumList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult BolumUpdate(int id)
        {
            var bolum = _context.Bolumler.Find(id);
            if (bolum == null) return NotFound();
            return View(bolum);
        }

        
        [HttpPost]
        public IActionResult BolumUpdate(Bolum bolum)
        {
            _context.Bolumler.Update(bolum);
            _context.SaveChanges();
            return RedirectToAction("BolumList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult BolumDelete(int id)
        {
            var bolum = _context.Bolumler
                .Include(b => b.Nobetler) // Nöbetler dahil
                .Include(b => b.Asistanlar) // Asistanlar dahil
                .Include(b => b.Hocalar) // Hocalar dahil
                .FirstOrDefault(b => b.Id == id);

            if (bolum == null) return NotFound();
            return View(bolum);
        }


        [HttpPost, ActionName("BolumDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var bolum = _context.Bolumler
                .Include(b => b.Nobetler)
                .FirstOrDefault(b => b.Id == id);

            if (bolum != null)
            {
                // Nöbetleri sil
                if (bolum.Nobetler != null)
                {
                    _context.Nobetler.RemoveRange(bolum.Nobetler);
                }

                // Bölüm kaydını sil
                _context.Bolumler.Remove(bolum);
                _context.SaveChanges();
            }

            return RedirectToAction("BolumList");
        }


    }
}
