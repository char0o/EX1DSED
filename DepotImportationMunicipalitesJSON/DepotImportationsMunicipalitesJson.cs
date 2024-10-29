using System.Text.Json;
using Entite;
using Microsoft.Extensions.Configuration;

namespace DepotImportationMunicipalitesJSON;

public class DepotImportationsMunicipalitesJson : IDepotImportationMunicipalites
{
    private readonly IConfiguration config;
    
    private const string COLCODE = "mcode";
    private const string COLNOM = "munnom";
    private const string COLREGION = "regadm";
    private const string COLWEB = "mweb";
    private const string COLDATELEC = "datelec";

    public DepotImportationsMunicipalitesJson(IConfiguration config)
    {
        this.config = config;
    }

    public IEnumerable<Municipalite> ImporterMunicipalites()
    {
        string chemin = config.GetSection("DepotSettings")["JSONPath"];
        if (!File.Exists(chemin))
        {
            throw new FileNotFoundException("JSON file could not be found.");
        }
        string jsonString = File.ReadAllText(chemin);
        
        using JsonDocument jsonDoc = JsonDocument.Parse(jsonString);
        JsonElement root = jsonDoc.RootElement;
        HashSet<Municipalite> municipImportees = new HashSet<Municipalite>();
        
        if (root.TryGetProperty("records", out JsonElement municipalites) &&
            municipalites.ValueKind == JsonValueKind.Array)
        {
            foreach (JsonElement municipalite in municipalites.EnumerateArray())
            {
                int code = municipalite.GetProperty(COLCODE).GetInt32();
                string nom = municipalite.GetProperty(COLNOM).GetString();
                string region = municipalite.GetProperty(COLREGION).GetString();
                string? web = municipalite.GetProperty(COLWEB).GetString();
                string? datelec = municipalite.GetProperty(COLDATELEC).GetString();
                web = web.Length == 0 ? null : web;
                DateTime? dateElection = datelec.Length == 0 ? null : DateTime.Parse(datelec);
                municipImportees.Add(new Municipalite()
                {
                    Code = code,
                    Nom = nom,
                    Region = region,
                    SiteWeb = web,
                    DateElection = dateElection
                });
            }
        }

        return municipImportees;
    }
}