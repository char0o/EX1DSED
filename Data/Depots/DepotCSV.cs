using Core.Entities;
using Core.Interfaces;

namespace Data.Repos;

public class DepotCSV : IDepotImportationMunicipalites
{
    private readonly StatistiquesImportation statistiquesImportation;
    private string csvFilePath = "";
    const int CODE = 0;
    const int NOM = 1;
    const int REGION = 15;
    const int SITEWEB = 9;
    const int DATEELECTION = 23;

    public DepotCSV(StatistiquesImportation statistiquesImportation, string csvFilePath)
    {
        this.statistiquesImportation = statistiquesImportation;
        this.csvFilePath = csvFilePath;
    }

    public IEnumerable<Municipalite> ImporterMunicipalites()
    {
        HashSet<Municipalite> municipalitiesImportees = new HashSet<Municipalite>();
        
        string[] lignes = System.IO.File.ReadAllLines(csvFilePath)
            .Skip(1)
            .ToArray();

        foreach (var ligne in lignes)
        {
            statistiquesImportation.NombreMunicipalitesImportees++;
            string[] champs = ligne.Split("\",\"");
            int code = int.Parse(champs[CODE].Substring(1));

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
            
            Municipalite nouvelle = new Municipalite()
            {
                Code = code,
                Nom = champs[NOM],
                SiteWeb = siteWeb,
                DateElection = dateElection,
                Region = champs[REGION],
                Actif = true
            };

            municipalitiesImportees.Add(nouvelle);
        }
        
        return municipalitiesImportees;
    }
}