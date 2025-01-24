using System;
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

        [Required]
        [Column("firmaId")]
        [JsonProperty("firmaId")]
        public int FirmaId { get; set; }

        [Required]
        [Column("adresId")]
        [JsonProperty("adresId")]
        public int AdresId { get; set; }

        [Required]
        [Column("formaId")]
        [JsonProperty("formaId")]
        public int FormaId { get; set; }

        [Required]
        [Column("terminOd")]
        [JsonProperty("terminOd")]
        public DateTime TerminOd { get; set; }

        [Required]
        [Column("terminDo")]
        [JsonProperty("terminDo")]
        public DateTime TerminDo { get; set; }

        [Column("trasaWedrowna")]
        [JsonProperty("trasaWedrowna")]
        public string? TrasaWedrowna { get; set; }

        [Column("kraj")]
        [JsonProperty("kraj")]
        public string? Kraj { get; set; }

        [ForeignKey(nameof(FirmaId))]
        [JsonProperty("firma")]
        public virtual Firma Firma { get; set; }

        [ForeignKey(nameof(AdresId))]
        [JsonProperty("adres")]
        public virtual Adres Adres { get; set; }

        [ForeignKey(nameof(FormaId))]
        [JsonProperty("forma")]
        public virtual Forma Forma { get; set; }
    }
}
