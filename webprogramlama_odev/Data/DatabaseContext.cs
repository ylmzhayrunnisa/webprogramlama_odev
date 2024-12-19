using webprogramlama_odev.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Runtime.InteropServices;

namespace webprogramlama_odev.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=HIRON\\HIRON;Integrated Security=True;Connect Timeout=30;Encrypt=False;Database=PediatriYonetimSistemi;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Asistan - Nobet ilişkisi (Cascade)
            modelBuilder.Entity<Nobet>()
                .HasOne(n => n.Asistan)
                .WithMany(a => a.Nobetler)
                .HasForeignKey(n => n.AsistanId)
                .OnDelete(DeleteBehavior.Cascade);

            // Asistan - Randevu ilişkisi (Cascade)
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Asistan)
                .WithMany(a => a.Randevular)
                .HasForeignKey(r => r.AsistanId)
                .OnDelete(DeleteBehavior.Cascade);

            // Hoca - Randevu ilişkisi (Cascade)
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Hoca)
                .WithMany(h => h.Randevular)
                .HasForeignKey(r => r.HocaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Bolum - Asistan ilişkisi (SetNull)
            modelBuilder.Entity<Asistan>()
                .HasOne(a => a.Bolum)
                .WithMany(b => b.Asistanlar)
                .HasForeignKey(a => a.BolumId)
                .OnDelete(DeleteBehavior.SetNull);

            // Bolum - Hoca ilişkisi (SetNull)
            modelBuilder.Entity<Hoca>()
                .HasOne(h => h.Bolum)
                .WithMany(b => b.Hocalar)
                .HasForeignKey(h => h.BolumId)
                .OnDelete(DeleteBehavior.SetNull);

            // Bolum - Nobet ilişkisi (Cascade)
            modelBuilder.Entity<Nobet>()
                .HasOne(n => n.Bolum)
                .WithMany(b => b.Nobetler)
                .HasForeignKey(n => n.BolumId)
                .OnDelete(DeleteBehavior.Cascade);

            // Hoca - HocaMüsaitlik ilişkisi (Cascade)
            modelBuilder.Entity<HocaMusaitlik>()
                .HasOne(r => r.Hoca)
                .WithMany(h => h.HocaMusaitlikler)
                .HasForeignKey(r => r.HocaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Bolum - Hasta ilişkisi (Cascade)
            modelBuilder.Entity<Hasta>()
                .HasOne(n => n.Bolum)
                .WithMany(b => b.Hastalar)
                .HasForeignKey(n => n.BolumId)
                .OnDelete(DeleteBehavior.Cascade);

            // Bolum - BolumAcilDurumlar ilişkisi (Cascade)
            modelBuilder.Entity<Bolum>()
                .HasMany(b => b.BolumAcilDurum)
                .WithOne(ba => ba.Bolum)
                .HasForeignKey(ba => ba.BolumId)  // Foreign key alan adı
                .OnDelete(DeleteBehavior.Cascade);  // Bolum silindiğinde BolumAcilDurum silinsin

            // BolumAcilDurum - AcilDurum ilişkisi (Yalnızca ilişkiyi sil)
            modelBuilder.Entity<BolumAcilDurum>()
                .HasOne(ba => ba.AcilDurum)
                .WithMany(ad => ad.BolumAcilDurum)
                .HasForeignKey(ba => ba.AcilDurumId)  // Foreign key alan adı
                .OnDelete(DeleteBehavior.Restrict);  // AcilDurum silinmesin, sadece ilişki silinsin




            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Asistan> Asistanlar { get; set; }
        public DbSet<Hoca> Hocalar { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        public DbSet<Nobet> Nobetler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<AcilDurum> AcilDurumlar { get; set; }
        public DbSet<Hasta> Hastalar { get; set; }
        public DbSet<HocaMusaitlik> HocaMusaitlikler { get; set; }
        public DbSet<BolumAcilDurum> BolumAcilDurum { get; set; }

    }

}
