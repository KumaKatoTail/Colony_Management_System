using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("OpiekunGrupa")]
    public class OpiekunGrupa
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("grupaId")]
        [JsonProperty("grupaId")]
        public int GrupaId { get; set; }

        [Column("opiekunId")]
        [JsonProperty("opiekunId")]
        public int OpiekunId { get; set; }

        [ForeignKey("GrupaId")]
        public Grupa Grupa { get; set; }

        [ForeignKey("OpiekunId")]
        public Opiekun Opiekun { get; set; }
    }
}
