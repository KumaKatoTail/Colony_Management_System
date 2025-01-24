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

        [ForeignKey("irodzId")]
        [Column("irodzId")]
        [JsonProperty("irodzId")]
        public int IrodzId { get; set; }

        [ForeignKey("idzieckoId")]
        [Column("idzieckoId")]
        [JsonProperty("idzieckoId")]
        public int IdzieckoId { get; set; }

        [Column("opis")]
        [JsonProperty("opis")]
        public string Opis { get; set; }

        public virtual Dziecko Dziecko { get; set; }
        public virtual RodzObs RodzObs { get; set; }
    }
}
