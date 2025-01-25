using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Grupa")]
    public class Grupa
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [Column("koloniaId")]
        [JsonProperty("koloniaId")]
        public int KoloniaId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("temat")]
        [JsonProperty("temat")]
        public string Temat { get; set; }

        [Required]
        [Column("opis")]
        [JsonProperty("opis")]
        public string Opis { get; set; }

       

        [Required]
        [Column("limit")]
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [ForeignKey(nameof(KoloniaId))]
        [JsonProperty("kolonia")]
        public virtual Kolonia Kolonia { get; set; }
    }
}
