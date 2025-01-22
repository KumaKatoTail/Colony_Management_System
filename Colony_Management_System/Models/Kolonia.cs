using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Kolonia")]
    public class Kolonia
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("firmaId")]
        [JsonProperty("firmaId")]
        public int FirmaId { get; set; }

        [Column("adresId")]
        [JsonProperty("adresId")]
        public int AdresId { get; set; }

        [Column("formaId")]
        [JsonProperty("formaId")]
        public int FormaId { get; set; }

        [Column("terminOd")]
        [JsonProperty("terminOd")]
        public DateTime TerminOd { get; set; }

        [Column("terminDo")]
        [JsonProperty("terminDo")]
        public DateTime TerminDo { get; set; }

        [Column("trasaWedrowna")]
        [JsonProperty("trasaWedrowna")]
        public string? TrasaWedrowna { get; set; }

        [Column("kraj")]
        [JsonProperty("kraj")]
        [StringLength(32)]
        public string? Kraj { get; set; }

        // Relacje
        [ForeignKey("FirmaId")]
        public Firma Firma { get; set; }

        [ForeignKey("AdresId")]
        public Adres Adres { get; set; }

        [ForeignKey("FormaId")]
        public Forma Forma { get; set; }

        public ICollection<Grupa> Grupa { get; set; }

        public Kolonia()
        {
            Grupa = new HashSet<Grupa>();
        }
    }
}
