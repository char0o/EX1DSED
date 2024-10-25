using Core.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Data
{
    public class TraitementCSV
    {
        private readonly IDepotMunicipalites depotMunicipalites;
        private readonly IDepotCSV depotCSV;
        private readonly StatistiquesImportation stats;

        public TraitementCSV(IDepotMunicipalites depotMunicipalites, StatistiquesImportation stats, IDepotCSV depotCSV)
        {
            this.depotMunicipalites = depotMunicipalites;
            this.depotCSV = depotCSV;
            this.stats = stats;
        }

        public void TraiterDepotCSV()
        {
            IEnumerable<Municipalite> municipalitesImportees = depotCSV.ImporterCSV("municipalites.csv");
            HashSet<int> codesExistants = new HashSet<int>(
                depotMunicipalites.ListerMunicipalities()
                    .Select(m => m.Code)
            );

            HashSet<int> codesImportees = new HashSet<int>(
                municipalitesImportees
                    .Select(m => m.Code)
            );

            foreach (Municipalite m in municipalitesImportees.Where(m => !codesExistants.Contains(m.Code)))
            {
                depotMunicipalites.AjouterMunicipalite(m);
                stats.NombreMunicipalitesAjoute++;
            }

            foreach (Municipalite municipalite in depotMunicipalites
                         .ListerMunicipalities()
                         .Where(m => !codesImportees.Contains(m.Code)))
            {
                depotMunicipalites.DesactiverMunicipalite(municipalite.Code);
                this.stats.NombreMunicipalitesDesactives++;
            }

            foreach (Municipalite municipalite in depotMunicipalites.ListerMunicipalities()
                         .Where(m => codesImportees.Contains(m.Code)))
            {
                if (municipalite.Actif == false)
                {
                    depotMunicipalites.ActiverMunicipalite(municipalite.Code);
                    this.stats.NombreMunicipalitesMisesAJour++;
                }
                else
                {
                    this.stats.NombreMunicipalitesNonModifiees++;
                }
            }
        }
    }
}