using Core.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Data
{
    public class ImportCSV : IImportCSV
    {
        private readonly IDepotMunicipalites depotMunicipalites;
        private readonly StatistiquesImportation stats;
        private readonly List<int> codes;

        const int CODE = 0;
        const int NOM = 1;
        const int REGION = 15;
        const int SITEWEB = 9;
        const int DATEELECTION = 23;

        public ImportCSV(IDepotMunicipalites depotMunicipalites, StatistiquesImportation stats)
        {
            this.codes = new List<int>();
            this.depotMunicipalites = depotMunicipalites;
            this.stats = stats;
        }

        public void ImporterCsv(string cheminFichier)
        {
            var lignes = System.IO.File.ReadAllLines(cheminFichier)
                .Skip(1)
                .ToArray();

            foreach (var ligne in lignes)
            {
                stats.NombreMunicipalitesImportees++;

                var champs = ligne.Split("\",\"");
                var code = int.Parse(champs[CODE].Substring(1));

                DateTime? dateElection = null;
                if (champs[DATEELECTION] != "")
                {
                    dateElection = DateTime.Parse(champs[DATEELECTION]);
                }

                string? siteWeb = null;
                if (champs[SITEWEB] != "")
                {
                    siteWeb = champs[SITEWEB];
                }
                
                Municipalite? municipalite = depotMunicipalites.ChercherMunicipaliteParCode(code);
                if (municipalite == null)
                {
                    Municipalite nouvelle = new Municipalite()
                    {
                        Code = code,
                        Nom = champs[NOM],
                        SiteWeb = siteWeb,
                        DateElection = dateElection,
                        Region = champs[REGION],
                        Actif = true
                    };
                    this.stats.NombreMunicipalitesAjoute++;
                }
                else
                {
                    this.codes.Add(code);
                }
            }
        }

        public void AjouterMunicipalites()
        {
            IEnumerable<Municipalite> municipalitesManquantes = depotMunicipalites
                .ListerMunicipalities()
                .Where(m => !this.codes.Contains(m.Code));

            foreach (Municipalite municipalite in municipalitesManquantes)
            {
                depotMunicipalites.DesactiverMunicipalite(municipalite.Code);
                this.stats.NombreMunicipalitesDesactives++;
            }

            IEnumerable<Municipalite> majMunicipalites = depotMunicipalites.ListerMunicipalities()
                .Where(m => this.codes.Contains(m.Code));

            foreach (Municipalite municipalite in majMunicipalites)
            {
                if (municipalite.Actif == false)
                {
                    municipalite.Actif = true;
                    this.stats.NombreMunicipalitesMisesAJour++;
                }
                else
                {
                    this.stats.NombreMunicipalitesNonModifiees++;
                }
            }

            Console.WriteLine(this.stats.ToString());
        }
    }
}