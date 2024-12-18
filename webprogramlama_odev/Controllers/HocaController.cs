using webprogramlama_odev.Data;
using webprogramlama_odev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webprogramlama_odev.Controllers
{
    public class HocaController : Controller
    {
        private readonly DatabaseContext _context;

        public HocaController(DatabaseContext context)
        {
            _context = context;

            if (!_context.Hocalar.Any())
            {
                // Resim dosyaları wwwroot/images/assistants içinde olmalı
                string ayseResmi = "/images/hImage/ogrUye1.jpeg";
                string ahmetResmi = "/images/hImage/ogrUye2.jpeg";
                string fatmaResmi = "/images/hImage/ogrUye3.jpeg";
                string mehmetResmi = "/images/hImage/ogrUye4.jpeg";
                string emineResmi = "/images/hImage/ogrUye5.jpeg";
                string hasanResmi = "/images/hImage/ogrUye6.jpeg";
                string elifResmi = "/images/hImage/ogrUye7.jpeg";
                string sibelResmi = "/images/hImage/ogrUye8.jpeg";
                string yusufResmi = "/images/hImage/ogrUye9.jpeg";
                string zeynepResmi = "/images/hImage/ogrUye10.jpeg";
                string ismailResmi = "/images/hImage/ogrUye11.jpeg";
                string leylaResmi = "/images/hImage/ogrUye12.jpeg";
                string tugbaResmi = "/images/hImage/ogrUye13.jpeg";
                string sevgiResmi = "/images/hImage/ogrUye14.jpeg";
                string omerResmi = "/images/hImage/ogrUye15.jpeg";


                _context.Hocalar.Add(new Hoca()
                {
                    
                    Ad = "Ayşe",
                    Soyad = "Kaya",                 
                    Telefon = "123456789",
                    Mail = "ayşe@gmail.com",
                    Resim = ayseResmi,

                });
                _context.Hocalar.Add(new Hoca()
                {
                    Ad = "Ahmet",
                    Soyad = "Yılmaz",
                    Telefon = "987654321",
                    Mail = "ahmet@gmail.com",
                    Resim = ahmetResmi,
                    
                });
                _context.Hocalar.Add(new Hoca()
                {

                    Ad = "Fatma",
                    Soyad = "Çelik",
                    Telefon = "223456788",
                    Mail = "fatma@gmail.com",
                    Resim = fatmaResmi,
                });

                _context.Hocalar.Add(new Hoca()
                {

                    Ad = "Mehmet",
                    Soyad = "Demir",
                    Telefon = "323466789",
                    Mail = "mehmet@gmail.com",
                    Resim = mehmetResmi,

                });
                _context.Hocalar.Add(new Hoca()
                {

                    Ad = "Emine",
                    Soyad = "Arslan",
                    Telefon = "623476789",
                    Mail = "emine@gmail.com",
                    Resim = emineResmi,

                });
                _context.Hocalar.Add(new Hoca()
                {
                
                    Ad = "Hasan",
                    Soyad = "Şahin",
                    Telefon = "723756789",
                    Mail = "hasan@gmail.com",
                    Resim = hasanResmi,
  
                });

                _context.Hocalar.Add(new Hoca()
                {
                    Ad = "Elif",
                    Soyad = "Yıldız",
                    Telefon = "153456789",
                    Mail = "elif@domain.com",
                    Resim = elifResmi,
 
                });
                _context.Hocalar.Add(new Hoca()
                {
          
                    Ad = "Sibel",
                    Soyad = "Özkan",
                    Telefon = "783456789",
                    Mail = "sibel@gmail.com",
                    Resim = sibelResmi,
                
                });
                _context.Hocalar.Add(new Hoca()
                {
              
                    Ad = "Yusuf",
                    Soyad = "Aydın",
                    Telefon = "148756789",
                    Mail = "yusuf@gmail.com",
                    Resim = yusufResmi,
             
                });

                _context.Hocalar.Add(new Hoca()
                {
                    Ad = "Zeynep",
                    Soyad = "Güneş",
                    Telefon = "923457789",
                    Mail = "zeynep@gmail.com",
                    Resim = zeynepResmi,
     
                });
                _context.Hocalar.Add(new Hoca()
                {
                    Ad = "İsmail",
                    Soyad = "Koç",
                    Telefon = "423756789",
                    Mail = "ismail@gmail.com",
                    Resim = ismailResmi,
                });
                _context.Hocalar.Add(new Hoca()
                {
                    Ad = "Leyla",
                    Soyad = "Taş",
                    Telefon = "623457789",
                    Mail = "leyla@gmail.com",
                    Resim = leylaResmi,
                });

                _context.Hocalar.Add(new Hoca()
                {
                    Ad = "Tuğba",
                    Soyad = "Aksoy",
                    Telefon = "745456789",
                    Mail = "tugba@gmail.com",
                    Resim = tugbaResmi,
                });
                _context.Hocalar.Add(new Hoca()
                {
                    Ad = "Sevgi",
                    Soyad = "Turan",
                    Telefon = "353456787",
                    Mail = "sevgi@gmail.com",
                    Resim = sevgiResmi,
                });
                _context.Hocalar.Add(new Hoca()
                {
                    Ad = "Ömer",
                    Soyad = "Kılıç",
                    Telefon = "684456789",
                    Mail = "omer@gmail.com",
                    Resim = omerResmi,
                });

                _context.SaveChanges();
            }
        }

        public IActionResult Hoca(string searchTerm, string sortOrder)
        {
            // Hoca bilgilerini bölümleriyle birlikte sorgulama
            var hocalar = _context.Hocalar
                .Include(h => h.Bolum)
                .AsQueryable();


            // Sonuçları listeye çevir ve görünümde göster
            return View(hocalar.ToList());
        }

        // Hocaların Listelenmesi
        [Authorize(Roles = "Admin")]
        public IActionResult HocaList(string searchTerm)
        {
            var hocalar = _context.Hocalar
                .Include(h => h.Bolum) 
                .OrderBy(h => h.Bolum.BolumAdi)  
                .AsQueryable();       


            // Sonuçları listeye çevir ve görünümde göster
            return View(hocalar.ToList());
        }

        // Yeni Hoca Ekleme - GET
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HocaAdd()
        {
            // Bölüm listesini ViewBag ile gönderiyoruz
            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View();
        }

        // Yeni Hoca Ekleme - POST
        [HttpPost]
        public IActionResult HocaAdd(Hoca hoca, IFormFile Resim)
        {
            // Resim varsa, resmi kaydediyoruz
            if (Resim != null)
            {
                var resimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "hImage", Resim.FileName);
                using (var fileStream = new FileStream(resimYolu, FileMode.Create))
                {
                    Resim.CopyTo(fileStream);
                }

                hoca.Resim = "/images/hImage/" + Resim.FileName;
            }
            // Eğer ModelState geçerli değilse, formu tekrar gösteriyoruz
            if (!ModelState.IsValid)
            {
                // Bölüm listesini ViewBag ile tekrar göndermek
                ViewBag.Bolumler = _context.Bolumler.ToList();
                return View(hoca);
            }
            // Yeni hocaı ekliyoruz
            _context.Hocalar.Add(hoca);
            _context.SaveChanges();

            return RedirectToAction("HocaList");
        }

        // Hoca Güncelleme - GET
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HocaUpdate(int id)
        {
            var hoca = _context.Hocalar.Find(id);
            if (hoca == null) return NotFound();

            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View(hoca);
        }

        // Hoca Güncelleme - POST
        [HttpPost]
        public IActionResult HocaUpdate(Hoca hoca, IFormFile Resim)
        {
            // Resim varsa, resmi kaydediyoruz
            if (Resim != null)
            {
                var resimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "hImage", Resim.FileName);
                using (var fileStream = new FileStream(resimYolu, FileMode.Create))
                {
                    Resim.CopyTo(fileStream);
                }

                hoca.Resim = "/images/hImage/" + Resim.FileName;
            }

            // Hoca bilgilerini güncelliyoruz
            _context.Hocalar.Update(hoca);
            _context.SaveChanges();

            return RedirectToAction("HocaList");
        }

        // Hoca Silme - GET (Onay Sayfası)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HocaDelete(int id)
        {
            var hoca = _context.Hocalar.Include(h => h.Bolum).FirstOrDefault(h => h.Id == id);
            if (hoca == null) return NotFound();
            return View(hoca);
        }

        // Hoca Silme - POST
        [HttpPost, ActionName("HocaDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var hoca = _context.Hocalar
                .Include(h => h.Randevular) // Randevular tablosundaki ilişkili verileri dahil et
                .FirstOrDefault(h => h.Id == id);

            if (hoca != null)
            {
                // Önce ilişkili randevuları sil
                _context.Randevular.RemoveRange(hoca.Randevular);

                // Daha sonra Hoca kaydını sil
                _context.Hocalar.Remove(hoca);
                _context.SaveChanges();

            }

            return RedirectToAction("HocaList");
        }



    }
}
