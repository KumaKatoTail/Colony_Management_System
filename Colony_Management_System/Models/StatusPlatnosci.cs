using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("statusPlatnosci")]
    public class StatusPlatnosci
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("status")]
        [JsonProperty("status")]
        [StringLength(100)]
        public string Nazwa { get; set; }

        public ICollection<Platnosc> Platnosci { get; set; }
    }
}
