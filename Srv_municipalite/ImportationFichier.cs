using Entite;

namespace Data
{
    public class ImportationFichier
    {
        private readonly IDepotMunicipalites depotMunicipalites;
        private readonly IDepotImportationMunicipalites depotImportation;

        public ImportationFichier(IDepotMunicipalites depotMunicipalites, 
            IDepotImportationMunicipalites depotImportation)
        {
            this.depotMunicipalites = depotMunicipalites;
            this.depotImportation = depotImportation;
        }

        public StatistiquesImportation TraiterDepotCSV()
        {
            StatistiquesImportation stats = new StatistiquesImportation();
            IEnumerable<Municipalite> municipalitesImportees = depotImportation.ImporterMunicipalites();
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
                stats.NombreMunicipalitesDesactives++;
            }

            /*foreach (Municipalite municipalite in depotMunicipalites.ListerMunicipalities()
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
            }*/
            return stats;
        }
    }
}