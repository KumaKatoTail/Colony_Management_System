using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Administrator")]
    public class Administrator
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [Column("kontoId")]
        [JsonProperty("kontoId")]
        public int KontoId { get; set; }

        [Required]
        [Column("firmaId")]
        [JsonProperty("firmaId")]
        public int FirmaId { get; set; }

        [ForeignKey(nameof(KontoId))]
        [JsonProperty("konto")]
        public virtual Konto Konto { get; set; }

        [ForeignKey(nameof(FirmaId))]
        [JsonProperty("firma")]
        public virtual Firma Firma { get; set; }
    }
}