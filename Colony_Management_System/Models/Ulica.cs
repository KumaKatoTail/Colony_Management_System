﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("Ulica")]
    public class Ulica
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("ulica")]
        [JsonProperty("ulica")]
        [StringLength(64)]
        public string Nazwa { get; set; }

        [Column("miastoId")]
        [JsonProperty("miastoId")]
        public int MiastoId { get; set; }

        [ForeignKey("MiastoId")]
        public Miasto Miasto { get; set; }

        public ICollection<Adres> Adresy { get; set; }
    }
}
