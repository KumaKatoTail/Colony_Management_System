using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("rodzajPlatnosci")]
    public class RodzajPlatnosci
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("status")]
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
