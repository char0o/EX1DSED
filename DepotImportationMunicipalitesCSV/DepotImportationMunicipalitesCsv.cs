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
        HashSet<Municipalite> municipalitiesImportees = new HashSet<Municipalite>();
        string? chemin = this.config.GetSection("DepotSettings")["CSVFilePath"];

        string[] lignes = File.ReadAllLines(chemin)
            .Skip(1)
            .ToArray();

        foreach (string ligne in lignes)
        {
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

            Municipalite nouvelle = new Municipalite(code, champs[NOM], champs[REGION], siteWeb, dateElection);
            
            municipalitiesImportees.Add(nouvelle);
        }

        return municipalitiesImportees;
    }
}