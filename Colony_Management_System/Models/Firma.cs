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

        [Column("adresId")]
        [JsonProperty("adresId")]
        public int AdresId { get; set; }

        [Column("nazwa")]
        [JsonProperty("nazwa")]
        public string Nazwa { get; set; }

        [ForeignKey("AdresId")]
        public Adres Adres { get; set; }

        public ICollection<Kolonia> Kolonia { get; set; }
        public ICollection<Administrator> Administrator { get; set; }
    }
}
