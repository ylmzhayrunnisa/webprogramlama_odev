using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webprogramlama_odev.Models;
using webprogramlama_odev.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace webprogramlama_odev.Controllers
{
    public class BolumAcilDurumController : Controller
    {
        private readonly DatabaseContext _context;

        public BolumAcilDurumController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult BolumAcilDurum(string searchTerm)
        {
            var bolumAcilDurumlar = _context.BolumAcilDurum
                .Include(bad => bad.Bolum)
                .Include(bad => bad.AcilDurum)
                .OrderBy(bad => bad.Bolum.BolumAdi) // Bölüm adına göre sıralama
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bolumAcilDurumlar = bolumAcilDurumlar.Where(bad =>
                    bad.Bolum.BolumAdi.Contains(searchTerm) ||
                    bad.AcilDurum.Konu.Contains(searchTerm));
            }

            return View(bolumAcilDurumlar.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult BolumAcilDurumList(string searchTerm)
        {
            var bolumAcilDurumlar = _context.BolumAcilDurum
                .Include(bad => bad.Bolum)
                .Include(bad => bad.AcilDurum)
                .OrderBy(bad => bad.Bolum.BolumAdi)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bolumAcilDurumlar = bolumAcilDurumlar.Where(bad =>
                    bad.Bolum.BolumAdi.Contains(searchTerm) ||
                    bad.AcilDurum.Konu.Contains(searchTerm));
            }

            return View(bolumAcilDurumlar.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult BolumAcilDurumAdd()
        {
            ViewBag.Bolumler = _context.Bolumler.ToList();
            ViewBag.AcilDurumlar = _context.AcilDurumlar.ToList();
            return View();
        }



        [HttpPost]
        public IActionResult BolumAcilDurumAdd(BolumAcilDurum bolumAcilDurum)
        {
            
                ViewBag.Bolumler = _context.Bolumler.ToList();
                ViewBag.AcilDurumlar = _context.AcilDurumlar.ToList();
                
            

            _context.BolumAcilDurum.Add(bolumAcilDurum);
            _context.SaveChanges();
            return RedirectToAction("BolumAcilDurumList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult BolumAcilDurumUpdate(int id)
        {
            var bolumAcilDurum = _context.BolumAcilDurum.Find(id);
            if (bolumAcilDurum == null) return NotFound();

            ViewBag.Bolumler = _context.Bolumler.ToList();
            ViewBag.AcilDurumlar = _context.AcilDurumlar.ToList();
            return View(bolumAcilDurum);
        }

        [HttpPost]
        public IActionResult BolumAcilDurumUpdate(BolumAcilDurum bolumAcilDurum)
        {
           
                ViewBag.Bolumler = _context.Bolumler.ToList();
                ViewBag.AcilDurumlar = _context.AcilDurumlar.ToList();
               
            

            _context.BolumAcilDurum.Update(bolumAcilDurum);
            _context.SaveChanges();
            return RedirectToAction("BolumAcilDurumList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult BolumAcilDurumDelete(int id)
        {
            var bolumAcilDurum = _context.BolumAcilDurum
                .Include(bad => bad.Bolum)
                .Include(bad => bad.AcilDurum)
                .FirstOrDefault(bad => bad.Id == id);

            if (bolumAcilDurum == null) return NotFound();
            return View(bolumAcilDurum);
        }

        [HttpPost, ActionName("BolumAcilDurumDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var bolumAcilDurum = _context.BolumAcilDurum.Find(id);
            if (bolumAcilDurum == null) return NotFound();

            _context.BolumAcilDurum.Remove(bolumAcilDurum);
            _context.SaveChanges();
            return RedirectToAction("BolumAcilDurumList");
        }
    }
}
