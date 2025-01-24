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

        [Required]
        [Column("dzieckoId")]
        [JsonProperty("dzieckoId")]
        public int DzieckoId { get; set; }

        [Required]
        [Column("rodzicId")]
        [JsonProperty("rodzicId")]
        public int RodzicId { get; set; }

        [ForeignKey(nameof(DzieckoId))]
        [JsonProperty("dziecko")]
        public virtual Dziecko Dziecko { get; set; }

        [ForeignKey(nameof(RodzicId))]
        [JsonProperty("rodzic")]
        public virtual Rodzic Rodzic { get; set; }
    }
}
