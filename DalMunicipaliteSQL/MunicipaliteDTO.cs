using Entite;
﻿using System.ComponentModel.DataAnnotations;

namespace DalMunicipaliteSQL
{
    public class MunicipaliteDTO
    {
        [Key]
        public int Code { get; set; }
        public string Nom { get; set; }
        public string Region { get; set; }
        public string? SiteWeb { get; set; }
        public DateTime? DateElection { get; set; }
        public bool Actif { get; set; }
        
        public Municipalite VersEntite()
        {
            return new Municipalite
            {
                Code = Code,
                Nom = Nom,
                Region = Region,
                SiteWeb = SiteWeb,
                DateElection = DateElection
            };
        }
    }
    

}
