using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webprogramlama_odev.Models
{
    public class HocaMusaitlik
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key

        [Required(ErrorMessage = "Hoca Adı alanı boş bırakılamaz.")]
        public int HocaId { get; set; } // Foreign Key      
        public Hoca Hoca { get; set; } // Navigation property

        [Required(ErrorMessage = "Tarih alanı boş bırakılamaz.")]
        public DateTime Tarih { get; set; } // DateTime, tarih için DateTime kullanılması daha doğru

        [Required(ErrorMessage = "Saat alanı boş bırakılamaz.")]
        public TimeSpan Saat { get; set; } // Saat için TimeSpan kullanılması daha doğru

    }
}
