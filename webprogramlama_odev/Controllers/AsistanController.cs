using Microsoft.AspNetCore.Mvc;
using webprogramlama_odev.Data;
using webprogramlama_odev.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace webprogramlama_odev.Controllers
{
    public class AsistanController : Controller
    {
        private readonly DatabaseContext _context;

        public AsistanController(DatabaseContext context)
        {
            _context = context;
            if (!_context.Asistanlar.Any())
            {
                // Resim dosyaları wwwroot/images/assistants içinde olmalı
                string adaResmi = "/images/asImage/as1.jpeg";
                string baranResmi = "/images/asImage/as2.jpeg";
                string batuResmi = "/images/asImage/as3.jpeg";
                string berkanResmi = "/images/asImage/as4.jpeg";
                string canerResmi = "/images/asImage/as5.jpeg";
                string badeResmi = "/images/asImage/as6.jpeg";
                string cansuResmi = "/images/asImage/as7.jpeg";
                string dehaResmi = "/images/asImage/as8.jpeg";
                string defneResmi = "/images/asImage/as9.jpeg";
                string eceResmi = "/images/asImage/as10.jpeg";
                string fatihResmi = "/images/asImage/as11.jpeg";
                string guneyResmi = "/images/asImage/as12.jpeg";
                string meteResmi = "/images/asImage/as13.jpeg";
                string poyrazResmi = "/images/asImage/as14.jpeg";
                string hulyaResmi = "/images/asImage/as15.jpeg";

                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Ada",
                    Soyad = "Kaya",
                    Telefon = "123456789",
                    Mail = "ayşe@gmail.com",
                    Resim = adaResmi,
                  

                });
                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Baran",
                    Soyad = "Yılmaz",
                    Telefon = "987654321",
                    Mail = "baran@gmail.com",
                    Resim = baranResmi,
                   

                });
                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Batu",
                    Soyad = "Çelik",
                    Telefon = "223456788",
                    Mail = "batu@gmail.com",
                    Resim = batuResmi,
                   

                });

                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Berkan",
                    Soyad = "Demir",
                    Telefon = "323466789",
                    Mail = "mehmet@gmail.com",
                    Resim = berkanResmi,

                });
                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Caner",
                    Soyad = "Arslan",
                    Telefon = "623476789",
                    Mail = "caner@gmail.com",
                    Resim = canerResmi,
                  

                });
                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Bade",
                    Soyad = "Şahin",
                    Telefon = "723756789",
                    Mail = "bade@gmail.com",
                    Resim = badeResmi,
    

                });

                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Cansu",
                    Soyad = "Yıldız",
                    Telefon = "153456789",
                    Mail = "cansu@domain.com",
                    Resim = cansuResmi,


                });
                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Deha",
                    Soyad = "Özkan",
                    Telefon = "783456789",
                    Mail = "deha@gmail.com",
                    Resim = dehaResmi,


                });
                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Defne",
                    Soyad = "Aydın",
                    Telefon = "148756789",
                    Mail = "defne@gmail.com",
                    Resim = defneResmi,
 

                });

                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Ece",
                    Soyad = "Güneş",
                    Telefon = "923457789",
                    Mail = "ece@gmail.com",
                    Resim = eceResmi,


                });
                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Fatih",
                    Soyad = "Koç",
                    Telefon = "423756789",
                    Mail = "fatih@gmail.com",
                    Resim = fatihResmi,


                });
                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Güney",
                    Soyad = "Taş",
                    Telefon = "623457789",
                    Mail = "guney@gmail.com",
                    Resim = guneyResmi,

                });

                _context.Asistanlar.Add(new Asistan()
                {
                    Ad = "Mete",
                    Soyad = "Aksoy",
                    Telefon = "745456789",
                    Mail = "mete@gmail.com",
                    Resim = meteResmi,


                });
                _context.Asistanlar.Add(new Asistan()
                {

                    Ad = "Poyraz",
                    Soyad = "Turan",
                    Telefon = "353456787",
                    Mail = "poyraz@gmail.com",
                    Resim = poyrazResmi,


                });
                _context.Asistanlar.Add(new Asistan()
                {

                    Ad = "Hülya",
                    Soyad = "Kılıç",
                    Telefon = "684456789",
                    Mail = "hulya@gmail.com",
                    Resim = hulyaResmi,
                });

                _context.SaveChanges();
            }
        }

        public IActionResult Asistan(string searchTerm, string sortOrder)
        {
            // Asistanlar bilgilerini bölümleriyle birlikte sorgulama
            var asistanlar = _context.Asistanlar
                .Include(h => h.Bolum)
                .AsQueryable();
     

            // Sonuçları listeye çevir ve görünümde göster
            return View(asistanlar.ToList());
        }

        // Asistanlarların Listelenmesi
        [Authorize(Roles = "Admin")]
        public IActionResult AsistanList(string searchTerm)
        {
            var asistanlar = _context.Asistanlar
                .Include(h => h.Bolum)
                .OrderBy(h => h.Bolum.BolumAdi)
                .AsQueryable();

            // Arama fonksiyonu
            if (!string.IsNullOrEmpty(searchTerm))
            {
                asistanlar = asistanlar.Where(h =>
                    h.Ad.Contains(searchTerm) ||
                    h.Soyad.Contains(searchTerm) ||
                    (h.Ad + " " + h.Soyad).Contains(searchTerm)
                );
            }

            return View(asistanlar.ToList());
        }

        // Yeni Asistanlar Ekleme - GET
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AsistanAdd()
        {
            // Bölüm listesini ViewBag ile gönderiyoruz
            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View();
        }

        // Yeni Asistanlar Ekleme - POST
        [HttpPost]
        public IActionResult AsistanAdd(Asistan asistan, IFormFile Resim)
        {
            // Resim kaydetme işlemi
            if (Resim != null)
            {
                var resimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "asImage", Resim.FileName);
                using (var fileStream = new FileStream(resimYolu, FileMode.Create))
                {
                    Resim.CopyTo(fileStream);
                }

                asistan.Resim = "/images/asImage/" + Resim.FileName;
            }

            // Eğer ModelState geçerli değilse, formu tekrar gösteriyoruz
            if (!ModelState.IsValid)
            {
                // Bölüm listesini ViewBag ile tekrar göndermek
                ViewBag.Bolumler = _context.Bolumler.ToList();
                return View(asistan);
            }

            // Yeni asistanı ekliyoruz
            _context.Asistanlar.Add(asistan);
            _context.SaveChanges();

            return RedirectToAction("AsistanList");
        }

        // Asistanlar Güncelleme - GET
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AsistanUpdate(int id)
        {
            var asistan = _context.Asistanlar.Find(id);
            if (asistan == null) return NotFound();

            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View(asistan);
        }

        // Asistanlar Güncelleme - POST
        [HttpPost]
        public IActionResult AsistanUpdate(Asistan asistan, IFormFile Resim)
        {
            // Resim varsa, resmi kaydediyoruz
            if (Resim != null)
            {
                var resimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "asImage", Resim.FileName);
                using (var fileStream = new FileStream(resimYolu, FileMode.Create))
                {
                    Resim.CopyTo(fileStream);
                }

                asistan.Resim = "/images/asImage/" + Resim.FileName;
            }

            // Asistanlar bilgilerini güncelliyoruz
            _context.Asistanlar.Update(asistan);
            _context.SaveChanges();

            return RedirectToAction("AsistanList");
        }

        // Asistanlar Silme - GET (Onay Sayfası)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AsistanDelete(int id)
        {
            var asistan = _context.Asistanlar.Include(h => h.Bolum).FirstOrDefault(h => h.Id == id);
            if (asistan == null) return NotFound();
            return View(asistan);
        }

        // Asistanlar Silme - POST
        [HttpPost, ActionName("AsistanDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var asistan = _context.Asistanlar
                .Include(h => h.Randevular)
                .FirstOrDefault(h => h.Id == id);

            // Asistanlar varsa, randevuları ve asistanı siliyoruz
            if (asistan != null)
            {
                _context.Randevular.RemoveRange(asistan.Randevular);
                _context.Asistanlar.Remove(asistan);
                _context.SaveChanges();
            }

            return RedirectToAction("AsistanList");
        }
    }
}
