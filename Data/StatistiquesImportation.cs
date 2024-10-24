using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class StatistiquesImportation
    {
        public int NombreMunicipalitesImportees { get; set; }
        public int NombreMunicipalitesMisesAJour { get; set; }
        public int NombreMunicipalitesNonModifiees { get; set; }
        public int NombreMunicipalitesDesactives { get; set; }
        public int NombreMunicipalitesAjoute { get; set; }

        public override string ToString()
        {
            return $"Nombre de municipalités importées : {NombreMunicipalitesImportees}\n" +
                   $"Nombre de municipalités mises à jour : {NombreMunicipalitesMisesAJour}\n" +
                   $"Nombre de municipalités non modifiées : {NombreMunicipalitesNonModifiees}\n" +
                   $"Nombre de municipalités désactivées : {NombreMunicipalitesDesactives}\n" +
                   $"Nombre de municipalités ajoutées : {NombreMunicipalitesAjoute}";
        }
    }
}
