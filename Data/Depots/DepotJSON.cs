using Core.Entities;
using Core.Interfaces;
using System.Net.Http;
using System.Text.Json;
using System.Linq;

namespace Data.Repos;

public class DepotJSON
{
    private readonly string lien = "";

    private const string COLCODE = "mcode";
    private const string COLNOM = "munnom";
    private const string COLREGION = "regadm";
    private const string COLWEB = "mweb";
    private const string COLDATELEC = "datelec";

    public DepotJSON(string lien)
    {
        this.lien = lien;
    }
    
    public async Task<IEnumerable<Municipalite>> ImporterMunicipalites()
    {
        using HttpClient client= new HttpClient();


        try
        {
            string jsonRespone = await client.GetStringAsync(lien);

            using JsonDocument jsonDoc = JsonDocument.Parse(jsonRespone);
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new HashSet<Municipalite>();
        }
        
    }
}