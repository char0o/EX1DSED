using Entite;
using Microsoft.Extensions.Configuration;

namespace DepotImportationMunicipalitesCSV;

public class DepotImportationMunicipalitesCsv : IDepotImportationMunicipalites
{
    private const int CODE = 0;
    private const int NOM = 1;
    private const int REGION = 15;
    private const int SITEWEB = 9;
    private const int DATEELECTION = 23;
    private readonly IConfiguration config;

    public DepotImportationMunicipalitesCsv(IConfiguration config)
    {
        this.config = config;
    }

    public IEnumerable<Municipalite> ImporterMunicipalites()
    {
        var municipalitiesImportees = new HashSet<Municipalite>();
        var chemin = config.GetSection("DepotSettings")["CSVFilePath"];

        var lignes = File.ReadAllLines(chemin)
            .Skip(1)
            .ToArray();

        foreach (var ligne in lignes)
        {
            var champs = ligne.Split("\",\"");
            var code = int.Parse(champs[CODE].Substring(1));

            DateTime? dateElection = null;
            if (champs[DATEELECTION] != "") dateElection = DateTime.Parse(champs[DATEELECTION]);

            string? siteWeb = null;
            if (champs[SITEWEB] != "") siteWeb = champs[SITEWEB];

            var nouvelle = new Municipalite
            {
                Code = code,
                Nom = champs[NOM],
                SiteWeb = siteWeb,
                DateElection = dateElection,
                Region = champs[REGION]
            };

            municipalitiesImportees.Add(nouvelle);
        }

        return municipalitiesImportees;
    }
}