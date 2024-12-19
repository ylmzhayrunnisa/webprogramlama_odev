using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace webprogramlama_odev.Models
{
    public class BolumAcilDurum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        [ForeignKey("BolumId")]
        public int? BolumId { get; set; } // Foreign Key
        public Bolum Bolum { get; set; } // Navigation property

        [ForeignKey("AcilDurumId")]
        public int? AcilDurumId { get; set; } // Foreign Key
        public AcilDurum AcilDurum { get; set; } // Navigation property
    }
}
