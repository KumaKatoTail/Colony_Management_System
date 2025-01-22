using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("forma")]
    public class Forma
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("forma")]
        [JsonProperty("forma")]
        [StringLength(20)]
        public string Nazwa { get; set; }

        public ICollection<Kolonia> Kolonie { get; set; }
    }
}
