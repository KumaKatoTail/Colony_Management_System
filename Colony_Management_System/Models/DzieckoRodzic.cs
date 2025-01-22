using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("DzieckoRodzic")]
    public class DzieckoRodzic
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("dzieckoId")]
        [JsonProperty("dzieckoId")]
        public int DzieckoId { get; set; }

        [Column("rodzicId")]
        [JsonProperty("rodzicId")]
        public int RodzicId { get; set; }

        [ForeignKey("DzieckoId")]
        public Dziecko Dziecko { get; set; }

        [ForeignKey("RodzicId")]
        public Rodzic Rodzic { get; set; }
    }
}
