﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Colony_Management_System.Models
{
    [Table("rodz_obs")]
    public class RodzajObserwacji
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("nazwa")]
        [JsonProperty("nazwa")]
        [StringLength(20)]
        public string Nazwa { get; set; }

        public ICollection<Obserwacja> Obserwacje { get; set; }
    }
}
