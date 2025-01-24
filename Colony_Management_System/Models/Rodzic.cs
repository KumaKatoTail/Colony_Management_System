using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Rodzic")]
    public class Rodzic
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("kontoId")]
        [JsonProperty("kontoId")]
        public int KontoId { get; set; }

        [Column("imie")]
        [JsonProperty("imie")]
        public string Imie { get; set; }

        [Column("nazwisko")]
        [JsonProperty("nazwisko")]
        public string Nazwisko { get; set; }

        [Column("telefon")]
        [JsonProperty("telefon")]
        public string Telefon { get; set; }

        [Column("mail")]
        [JsonProperty("mail")]
        public string Mail { get; set; }

        [Column("adresId")]
        [JsonProperty("adresId")]
        public int AdresId { get; set; }

        // Relacje
        [ForeignKey("KontoId")]
        public virtual Konto Konto { get; set; }

        [ForeignKey("AdresId")]
        public virtual Adres Adres { get; set; }
    }
}
