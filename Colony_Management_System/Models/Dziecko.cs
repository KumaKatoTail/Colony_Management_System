using System;
using System.Collections.Generic;
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

        [Column("imie")]
        [JsonProperty("imie")]
        [StringLength(64)]
        public string Imie { get; set; }

        [Column("nazwisko")]
        [JsonProperty("nazwisko")]
        [StringLength(64)]
        public string Nazwisko { get; set; }

        [Column("data_ur")]
        [JsonProperty("dataUrodzenia")]
        public DateTime DataUrodzenia { get; set; }

        [Column("pesel")]
        [JsonProperty("pesel")]
        [StringLength(11)]
        public string Pesel { get; set; }

        [Column("adresId")]
        [JsonProperty("adresId")]
        public int AdresId { get; set; }

        [ForeignKey("AdresId")]
        public Adres Adres { get; set; }

        public ICollection<DzieckoRodzic> DzieckoRodzice { get; set; }
        public ICollection<KoloniaDziecko> KoloniaDzieci { get; set; }
        public ICollection<Obserwacja> Obserwacje { get; set; }
    }
}
