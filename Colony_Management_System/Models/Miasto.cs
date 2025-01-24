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

        [Required]
        [Column("miasto")]
        [JsonProperty("miasto")]
        public string MiastoNazwa { get; set; }

        [Required]
        [Column("kod")]
        [JsonProperty("kod")]
        public string Kod { get; set; }
    }
}
