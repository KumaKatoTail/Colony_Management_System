using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Adres")]
    public class Adres
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [Column("ulicaId")]
        [JsonProperty("ulicaId")]
        public int UlicaId { get; set; }

        [Required]
        [Column("nr_domu")]
        [JsonProperty("nr_domu")]
        public int NrDomu { get; set; }

        [Column("nr_miesz")]
        [JsonProperty("nr_miesz")]
        public int? NrMiesz { get; set; }

        [ForeignKey(nameof(UlicaId))]
        [JsonProperty("ulica")]
        public virtual Ulica Ulica { get; set; }
    }
}
