using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Dziecko")]
    public class Dziecko
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        [Column("imie")]
        [JsonProperty("imie")]
        public string Imie { get; set; }

        [Required]
        [MaxLength(64)]
        [Column("nazwisko")]
        [JsonProperty("nazwisko")]
        public string Nazwisko { get; set; }

        [Required]
        [Column("data_ur")]
        [JsonProperty("data_ur")]
        public DateTime DataUrodzenia { get; set; }

        [Required]
        [MaxLength(11)]
        [MinLength(11)]
        [Column("pesel")]
        [JsonProperty("pesel")]
        public string Pesel { get; set; }

        [Required]
        [Column("adresId")]
        [JsonProperty("adresId")]
        public int AdresId { get; set; }

        [ForeignKey(nameof(AdresId))]
        [JsonProperty("adres")]
        public virtual Adres Adres { get; set; }
    }
}
