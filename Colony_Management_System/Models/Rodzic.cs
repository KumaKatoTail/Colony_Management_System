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
        [StringLength(64)]
        public string Imie { get; set; }

        [Column("nazwisko")]
        [JsonProperty("nazwisko")]
        [StringLength(64)]
        public string Nazwisko { get; set; }

        [Column("telefon")]
        [JsonProperty("telefon")]
        [StringLength(12)]
        public string Telefon { get; set; }

        [Column("mail")]
        [JsonProperty("mail")]
        [StringLength(128)]
        public string Mail { get; set; }

        [Column("adresId")]
        [JsonProperty("adresId")]
        public int AdresId { get; set; }

        [ForeignKey("KontoId")]
        public Konto Konto { get; set; }

        [ForeignKey("AdresId")]
        public Adres Adres { get; set; }

        public ICollection<DzieckoRodzic> DzieckoRodzic { get; set; }
    }
}
