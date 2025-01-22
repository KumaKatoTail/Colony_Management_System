using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("KoloniaDziecko")]
    public class KoloniaDziecko
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("dzieckoId")]
        [JsonProperty("dzieckoId")]
        public int DzieckoId { get; set; }

        [Column("koloniaId")]
        [JsonProperty("koloniaId")]
        public int KoloniaId { get; set; }

        [Column("statusId")]
        [JsonProperty("statusId")]
        public int StatusId { get; set; }

        [ForeignKey("DzieckoId")]
        public Dziecko Dziecko { get; set; }

        [ForeignKey("KoloniaId")]
        public Kolonia Kolonia { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        public ICollection<Platnosc> Platnosci { get; set; }
    }
}
