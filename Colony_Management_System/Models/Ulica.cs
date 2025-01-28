using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Ulica")]
    public class Ulica
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("ulica")]
        [JsonProperty("ulica")]
        public string ulica { get; set; }

        [ForeignKey("Miasto")]
        [Column("miastoId")]
        [JsonProperty("miastoId")]
        public int MiastoId { get; set; }

        public virtual Miasto Miasto { get; set; }
    }
}
