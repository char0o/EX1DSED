﻿using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Municipalite
    {
        [Key]
        public int Code { get; set; }
        public string Nom { get; set; }
        public string Region { get; set; }
        public string? SiteWeb { get; set; }
        public DateTime? DateElection { get; set; }
        public bool Actif { get; set; }
    }
}
