using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("RodzajPlatnosci")]
    public class RodzajPlatnosci
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("nazwa")]
        [JsonProperty("nazwa")]
        [StringLength(64)]
        public string Nazwa { get; set; }

        public ICollection<Platnosc> Platnosci { get; set; }
    }
}
