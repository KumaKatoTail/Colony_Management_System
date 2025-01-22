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

        [Column("kontoId")]
        [JsonProperty("kontoId")]
        public int KontoId { get; set; }

        [Column("firmaId")]
        [JsonProperty("firmaId")]
        public int FirmaId { get; set; }

        [ForeignKey("KontoId")]
        public Konto Konto { get; set; }

        [ForeignKey("FirmaId")]
        public Firma Firma { get; set; }
    }
}
