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

        [ForeignKey("kontoId")]
        [Column("kontoId")]
        [JsonProperty("kontoId")]
        public int KontoId { get; set; }

        [Column("telefon")]
        [JsonProperty("telefon")]
        public string Telefon { get; set; }

        [Column("imie")]
        [JsonProperty("imie")]
        public string Imie { get; set; }

        [Column("nazwisko")]
        [JsonProperty("nazwisko")]
        public string Nazwisko { get; set; }

        public virtual Konto Konto { get; set; }
    }
}
