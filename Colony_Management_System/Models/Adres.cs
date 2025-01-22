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

        [Column("ulicaId")]
        [JsonProperty("ulicaId")]
        public int UlicaId { get; set; }

        [Column("nr_domu")]
        [JsonProperty("nrDomu")]
        public int NrDomu { get; set; }

        [Column("nr_miesz")]
        [JsonProperty("nrMieszkania")]
        public int? NrMieszkania { get; set; }

        [ForeignKey("UlicaId")]
        public Ulica Ulica { get; set; }

        public ICollection<Dziecko> Dzieci { get; set; }
        public ICollection<Rodzic> Rodzice { get; set; }
        public ICollection<Firma> Firmy { get; set; }
    }
}
