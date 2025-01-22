using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Obserwacja")]
    public class Obserwacja
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("irodzId")]
        [JsonProperty("irodzId")]
        public int RodzajObserwacjiId { get; set; }

        [Column("idzieckoId")]
        [JsonProperty("idzieckoId")]
        public int DzieckoId { get; set; }

        [Column("opis")]
        [JsonProperty("opis")]
        public string Opis { get; set; }

        [ForeignKey("RodzajObserwacjiId")]
        public RodzajObserwacji RodzajObserwacji { get; set; }

        [ForeignKey("DzieckoId")]
        public Dziecko Dziecko { get; set; }
    }
}
