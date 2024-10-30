using System.Text.Json;
using Entite;
using Microsoft.Extensions.Configuration;

namespace DepotImportationMunicipalitesJSON;

public class DepotImportationsMunicipalitesJson : IDepotImportationMunicipalites
{
    private const string COLCODE = "mcode";
    private const string COLNOM = "munnom";
    private const string COLREGION = "regadm";
    private const string COLWEB = "mweb";
    private const string COLDATELEC = "datelec";
    private readonly IConfiguration config;

    public DepotImportationsMunicipalitesJson(IConfiguration config)
    {
        this.config = config;
    }

    public IEnumerable<Municipalite> ImporterMunicipalites()
    {
        var chemin = config.GetSection("DepotSettings")["JSONPath"];
        if (!File.Exists(chemin)) throw new FileNotFoundException("JSON file could not be found.");
        var jsonString = File.ReadAllText(chemin);

        using var jsonDoc = JsonDocument.Parse(jsonString);
        var root = jsonDoc.RootElement;
        var municipImportees = new HashSet<Municipalite>();

        if (root.TryGetProperty("result", out var result))
            if (result.TryGetProperty("records", out var municipalites))
                foreach (var municipalite in municipalites.EnumerateArray())
                {
                    var codeStr = municipalite.GetProperty(COLCODE).GetString();
                    var nom = municipalite.GetProperty(COLNOM).GetString();
                    var region = municipalite.GetProperty(COLREGION).GetString();
                    var web = municipalite.GetProperty(COLWEB).GetString();
                    var datelec = municipalite.GetProperty(COLDATELEC).GetString();

                    if (string.IsNullOrWhiteSpace(codeStr) || string.IsNullOrWhiteSpace(nom) ||
                        string.IsNullOrWhiteSpace(region))
                        throw new FormatException("Invalid JSON format.");

                    var code = Convert.ToInt32(codeStr);
                    DateTime? dateElection = Convert.ToDateTime(datelec);

                    municipImportees.Add(new Municipalite
                    {
                        Code = code,
                        Nom = nom,
                        Region = region,
                        SiteWeb = web,
                        DateElection = dateElection
                    });
                }

        return municipImportees;
    }
}