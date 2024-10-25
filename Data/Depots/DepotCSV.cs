using Core.Entities;

namespace Data.Repos;

public class DepotCSV : IDepotCSV
{
    private readonly StatistiquesImportation statistiquesImportation;
    const int CODE = 0;
    const int NOM = 1;
    const int REGION = 15;
    const int SITEWEB = 9;
    const int DATEELECTION = 23;

    public DepotCSV(StatistiquesImportation statistiquesImportation)
    {
        this.statistiquesImportation = statistiquesImportation;
    }

    public IEnumerable<Municipalite> ImporterCSV(string csvFilePath)
    {
        HashSet<Municipalite> municipalitiesImportees = new HashSet<Municipalite>();
        
        var lignes = System.IO.File.ReadAllLines(csvFilePath)
            .Skip(1)
            .ToArray();

        foreach (var ligne in lignes)
        {
            statistiquesImportation.NombreMunicipalitesImportees++;
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