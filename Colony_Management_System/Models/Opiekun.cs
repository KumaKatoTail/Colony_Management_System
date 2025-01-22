using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Opiekun")]
    public class Opiekun
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("kontoId")]
        [JsonProperty("kontoId")]
        public int KontoId { get; set; }

        [Column("telefon")]
        [JsonProperty("telefon")]
        [StringLength(12)]
        public string Telefon { get; set; }

        [Column("imie")]
        [JsonProperty("imie")]
        [StringLength(32)]
        public string Imie { get; set; }

        [Column("nazwisko")]
        [JsonProperty("nazwisko")]
        [StringLength(32)]
        public string Nazwisko { get; set; }

        [ForeignKey("KontoId")]
        public Konto Konto { get; set; }

        public ICollection<OpiekunGrupa> Grupy { get; set; }
    }
}
