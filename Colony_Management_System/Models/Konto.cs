using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Konto")]
    public class Konto
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [Column("email")]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [Column("haslo")]
        [JsonProperty("haslo")]
        public string Haslo { get; set; }

        [Required]
        [Column("uprId")]
        [JsonProperty("uprId")]
        public int UprId { get; set; }

        [ForeignKey(nameof(UprId))]
        [JsonProperty("upr")]
        public virtual Upr Upr { get; set; }
    }
}
