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

        [Required]
        [MaxLength(20)]
        [Column("forma")]
        [JsonProperty("forma")]
        public string RodzajWypoczynku { get; set; }
    }
}
