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

        [Column("grupaId")]
        [JsonProperty("grupaId")]
        public int GrupaId { get; set; }

        // Navigation properties
        public Dziecko Dziecko { get; set; }
        public Kolonia Kolonia { get; set; }
        public Status Status { get; set; }
        public Grupa Grupa { get; set; } // Add the navigation property for Grupa

        // One-to-many relationship with Platnosc
        public ICollection<Platnosc> Platnosc { get; set; }
    }
}
