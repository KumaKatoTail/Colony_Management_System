using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Miasto")]
    public class Miasto
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("miasto")]
        [JsonProperty("miasto")]
        [StringLength(64)]
        public string MiastoName { get; set; }

        [Column("kod")]
        [JsonProperty("kod")]
        [StringLength(6)]
        public string Kod { get; set; }

        public ICollection<Ulica> Ulica { get; set; }
    }
}
