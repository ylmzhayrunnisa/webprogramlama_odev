﻿using Microsoft.AspNetCore.Mvc;
using webprogramlama_odev.Models;
using webprogramlama_odev.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace webprogramlama_odev.Controllers
{
    public class NobetController : Controller
    {
        private readonly DatabaseContext _context;

        public NobetController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult NobetList()
        {
            var nobetler = _context.Nobetler.Include(n => n.Asistan).Include(n => n.Bolum).ToList();
            return View(nobetler);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult NobetAdd()
        {
            ViewBag.Asistanlar = _context.Asistanlar.ToList();
            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult NobetAdd(Nobet nobet)
        {
            if (nobet == null)
            {
                throw new Exception("Nobet nesnesi null.");
            }

            // Çakışma kontrolü
            var mevcutNobet = _context.Nobetler
                .Where(n => n.AsistanId == nobet.AsistanId)
                .Where(n =>
                    (nobet.BaslamaTarihi >= n.BaslamaTarihi && nobet.BaslamaTarihi <= n.BitisTarihi) ||
                    (nobet.BitisTarihi >= n.BaslamaTarihi && nobet.BitisTarihi <= n.BitisTarihi))
                .FirstOrDefault();

            if (mevcutNobet != null)
            {
                // Hata mesajını ModelState'e ekleyin
                ModelState.AddModelError("", "Bu asistan belirtilen tarihler arasında zaten başka bir nöbete atanmış.");

                // ViewBag ile gerekli listeleri tekrar ekleyin
                ViewBag.Asistanlar = _context.Asistanlar.ToList();
                ViewBag.Bolumler = _context.Bolumler.ToList();

                // Kullanıcıya mevcut model ile tekrar aynı sayfayı döndürün
                return View(nobet);
            }

            // Nöbeti ekle
            _context.Nobetler.Add(nobet);
            _context.SaveChanges();
            return RedirectToAction(nameof(NobetList));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult NobetUpdate(int id) 
        {
            var nobet = _context.Nobetler.Find(id);
            if (nobet == null) return NotFound();

            ViewBag.Asistanlar = _context.Asistanlar.ToList();
            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View(nobet);
        }

        [HttpPost]
        public IActionResult NobetUpdate(Nobet nobet) 
        {

            if (nobet == null)
            {
                throw new Exception("Nobet nesnesi null.");
            }

            // Çakışma kontrolü
            var mevcutNobet = _context.Nobetler
                .Where(n => n.AsistanId == nobet.AsistanId)
                .Where(n =>
                    (nobet.BaslamaTarihi >= n.BaslamaTarihi && nobet.BaslamaTarihi <= n.BitisTarihi) ||
                    (nobet.BitisTarihi >= n.BaslamaTarihi && nobet.BitisTarihi <= n.BitisTarihi))
                .FirstOrDefault();

            if (mevcutNobet != null)
            {
                // Hata mesajını ModelState'e ekleyin
                ModelState.AddModelError("", "Bu asistan belirtilen tarihler arasında zaten başka bir nöbete atanmış.");

                // ViewBag ile gerekli listeleri tekrar ekleyin
                ViewBag.Asistanlar = _context.Asistanlar.ToList();
                ViewBag.Bolumler = _context.Bolumler.ToList();

                // Kullanıcıya mevcut model ile tekrar aynı sayfayı döndürün
                return View(nobet);
            }

            // Nöbeti ekle
            _context.Nobetler.Update(nobet);
            _context.SaveChanges();
            return RedirectToAction(nameof(NobetList));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult NobetDelete(int id)
        {
            var nobet = _context.Nobetler.Find(id);
            if (nobet == null) return NotFound();

            ViewBag.Asistanlar = _context.Asistanlar.ToList();
            ViewBag.Bolumler = _context.Bolumler.ToList();

            return View(nobet);
        }

        // Hoca Silme - POST
        [HttpPost, ActionName("NobetDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var nobet = _context.Nobetler.Find(id);
            if (nobet != null)
            {
                _context.Nobetler.Remove(nobet);
                _context.SaveChanges();

            }
            return RedirectToAction("NobetList");

        }

    }
}
