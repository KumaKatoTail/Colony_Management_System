using System;
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

        [ForeignKey("koloniaDieckoId")]
        [Column("koloniaDieckoId")]
        [JsonProperty("koloniaDieckoId")]
        public int KoloniaDieckoId { get; set; }

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
        public string NumerRef { get; set; }

        [Column("idTranzakcja")]
        [JsonProperty("idTranzakcja")]
        public int IdTranzakcja { get; set; }

        [Column("bramka")]
        [JsonProperty("bramka")]
        public string Bramka { get; set; }

        [Column("oplataPosr")]
        [JsonProperty("oplataPosr")]
        public double OplataPosr { get; set; }

        [ForeignKey("Rodzic_id")]
        [Column("Rodzic_id")]
        [JsonProperty("Rodzic_id")]
        public int RodzicId { get; set; }

        [ForeignKey("waluta_id")]
        [Column("waluta_id")]
        [JsonProperty("waluta_id")]
        public int WalutaId { get; set; }

        [ForeignKey("rodzajPlatnosci_id")]
        [Column("rodzajPlatnosci_id")]
        [JsonProperty("rodzajPlatnosci_id")]
        public int RodzajPlatnosciId { get; set; }

        [ForeignKey("status_id")]
        [Column("status_id")]
        [JsonProperty("status_id")]
        public int StatusId { get; set; }

        public virtual KoloniaDziecko KoloniaDziecko { get; set; }
        public virtual Rodzic Rodzic { get; set; }
        public virtual RodzajPlatnosci RodzajPlatnosci { get; set; }
        public virtual StatusPlatnosci StatusPlatnosci { get; set; }
        public virtual Waluta Waluta { get; set; }
    }
}
