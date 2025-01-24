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

        [ForeignKey("grupaId")]
        [Column("grupaId")]
        [JsonProperty("grupaId")]
        public int GrupaId { get; set; }

        [ForeignKey("opiekunId")]
        [Column("opiekunId")]
        [JsonProperty("opiekunId")]
        public int OpiekunId { get; set; }

        public virtual Grupa Grupa { get; set; }
        public virtual Opiekun Opiekun { get; set; }
    }
}
