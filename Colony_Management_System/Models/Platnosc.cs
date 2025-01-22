using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Platnosc")]
    public class Platnosc
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("koloniaDieckoId")]
        [JsonProperty("koloniaDieckoId")]
        public int KoloniaDzieckoId { get; set; }

        [Column("czyFaktura")]
        [JsonProperty("czyFaktura")]
        public bool CzyFaktura { get; set; }

        [Column("kwota")]
        [JsonProperty("kwota")]
        public double Kwota { get; set; }

        [Column("dataPlatnosci")]
        [JsonProperty("dataPlatnosci")]
        public DateTime DataPlatnosci { get; set; }

        [Column("opis")]
        [JsonProperty("opis")]
        public string Opis { get; set; }

        [Column("numerRef")]
        [JsonProperty("numerRef")]
        [StringLength(255)]
        public string NumerRef { get; set; }

        [Column("idTranzakcja")]
        [JsonProperty("idTranzakcja")]
        public int IdTranzakcja { get; set; }

        [Column("bramka")]
        [JsonProperty("bramka")]
        [StringLength(255)]
        public string Bramka { get; set; }

        [Column("oplataPosr")]
        [JsonProperty("oplataPosr")]
        public double OplataPosr { get; set; }

        [Column("Rodzic_id")]
        [JsonProperty("Rodzic_id")]
        public int RodzicId { get; set; }

        [Column("waluta_id")]
        [JsonProperty("waluta_id")]
        public int WalutaId { get; set; }

        [Column("rodzajPlatnosci_id")]
        [JsonProperty("rodzajPlatnosci_id")]
        public int RodzajPlatnosciId { get; set; }

        [Column("status_id")]
        [JsonProperty("status_id")]
        public int StatusId { get; set; }

        [ForeignKey("KoloniaDzieckoId")]
        public KoloniaDziecko KoloniaDziecko { get; set; }

        [ForeignKey("RodzicId")]
        public Rodzic Rodzic { get; set; }

        [ForeignKey("RodzajPlatnosciId")]
        public RodzajPlatnosci RodzajPlatnosci { get; set; }

        [ForeignKey("StatusId")]
        public StatusPlatnosci StatusPlatnosci { get; set; }

        [ForeignKey("WalutaId")]
        public Waluta Waluta { get; set; }
    }
}
