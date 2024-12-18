using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webprogramlama_odev.Models
{
    public class AcilDurum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Konu { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih {  get; set; }
        public TimeSpan Saat { get; set; }
    }
}
