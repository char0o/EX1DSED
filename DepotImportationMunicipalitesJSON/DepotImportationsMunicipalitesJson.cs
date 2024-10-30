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
        string? chemin = this.config.GetSection("DepotSettings")["JSONPath"];
        if (!File.Exists(chemin))
        {
            throw new FileNotFoundException("JSON file could not be found.");
        }

        string jsonString = File.ReadAllText(chemin);

        using JsonDocument jsonDoc = JsonDocument.Parse(jsonString);
        JsonElement root = jsonDoc.RootElement;
        HashSet<Municipalite> municipImportees = new HashSet<Municipalite>();

        if (root.TryGetProperty("result", out JsonElement result))
        {
            if (result.TryGetProperty("records", out JsonElement municipalites))
            {
                foreach (JsonElement municipalite in municipalites.EnumerateArray())
                {
                    string? codeStr = municipalite.GetProperty(COLCODE).GetString();
                    string? nom = municipalite.GetProperty(COLNOM).GetString();
                    string? region = municipalite.GetProperty(COLREGION).GetString();
                    string? web = municipalite.GetProperty(COLWEB).GetString();
                    string? datelec = municipalite.GetProperty(COLDATELEC).GetString();

                    if (string.IsNullOrWhiteSpace(codeStr) || string.IsNullOrWhiteSpace(nom) ||
                        string.IsNullOrWhiteSpace(region))
                    {
                        throw new FormatException("Invalid JSON format.");
                    }

                    int code = Convert.ToInt32(codeStr);
                    DateTime? dateElection = Convert.ToDateTime(datelec);

                    municipImportees.Add(new Municipalite(code, nom, region, web, dateElection));
                }
            }
        }

        return municipImportees;
    }
}