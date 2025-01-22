using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Konto")]
    public class Konto
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("email")]
        [JsonProperty("email")]
        [StringLength(128)]
        public string Email { get; set; }

        [Column("haslo")]
        [JsonProperty("haslo")]
        public string Haslo { get; set; }

        [Column("uprId")]
        [JsonProperty("uprId")]
        public int UprId { get; set; }

        [ForeignKey("UprId")]
        public Upr Upr { get; set; }

        public ICollection<Administrator> Administratorzy { get; set; }
        public ICollection<Opiekun> Opiekunowie { get; set; }
        public ICollection<Rodzic> Rodzice { get; set; }
    }
}
