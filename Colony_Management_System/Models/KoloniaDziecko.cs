using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("KoloniaDziecko")]
    public class KoloniaDziecko
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [Column("dzieckoId")]
        [JsonProperty("dzieckoId")]
        public int DzieckoId { get; set; }

        [Required]
        [Column("grupaId")]
        [JsonProperty("grupaId")]
        public int GrupaId { get; set; }

        [Required]
        [Column("statusId")]
        [JsonProperty("statusId")]
        public int StatusId { get; set; }

        [Required]
        [Column("dataZapisu")]
        [JsonProperty("dataZapisu")]
        public int DataZapisu { get; set; }

        [ForeignKey(nameof(DzieckoId))]
        [JsonProperty("dziecko")]
        public virtual Dziecko Dziecko { get; set; }

        [ForeignKey(nameof(GrupaId))]
        [JsonProperty("grupa")]
        public virtual Grupa Grupa { get; set; }

        [ForeignKey(nameof(StatusId))]
        [JsonProperty("status")]
        public virtual Status Status { get; set; }
    }
}
