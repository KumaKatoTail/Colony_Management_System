using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Waluta")]
    public class Waluta
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("nazwa")]
        [JsonProperty("nazwa")]
        [StringLength(64)]
        public string Nazwa { get; set; }

        [Column("symbol")]
        [JsonProperty("symbol")]
        [StringLength(10)]
        public string Symbol { get; set; }

        public ICollection<Platnosc> Platnosci { get; set; }
    }
}
