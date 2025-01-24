using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Firma")]
    public class Firma
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [Column("adresId")]
        [JsonProperty("adresId")]
        public int AdresId { get; set; }

        [Required]
        [MaxLength(11)]
        [Column("nazwa")]
        [JsonProperty("nazwa")]
        public string Nazwa { get; set; }

        [ForeignKey(nameof(AdresId))]
        [JsonProperty("adres")]
        public virtual Adres Adres { get; set; }
    }
}
