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

        [Column("koloniaId")]
        [JsonProperty("koloniaId")]
        public int KoloniaId { get; set; }

        [Column("temat")]
        [JsonProperty("temat")]
        [StringLength(255)]
        public string Temat { get; set; }

        [Column("opis")]
        [JsonProperty("opis")]
        public string Opis { get; set; }

        [Column("limit")]
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [ForeignKey("KoloniaId")]
        public Kolonia Kolonia { get; set; }

        public ICollection<KoloniaDziecko> KoloniaDziecko { get; set; }
        public ICollection<OpiekunGrupa> OpiekunGrupa { get; set; }
    }
}
