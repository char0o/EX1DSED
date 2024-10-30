using System.Diagnostics.CodeAnalysis;
using Entite;

namespace Data;

public class ImportationFichier
{
    private readonly IDepotImportationMunicipalites depotImportation;
    private readonly IDepotMunicipalites depotMunicipalites;

    public ImportationFichier(IDepotMunicipalites depotMunicipalites,
        IDepotImportationMunicipalites depotImportation)
    {
        this.depotMunicipalites = depotMunicipalites;
        this.depotImportation = depotImportation;
    }

    [SuppressMessage("ReSharper.DPA", "DPA0005: Database issues")]
    public StatistiquesImportation TraiterFichier()
    {
        StatistiquesImportation stats = new();

        IEnumerable<Municipalite> municipalitesImportees = this.depotImportation.ImporterMunicipalites();
        IEnumerable<Municipalite> municipalitesActives = this.depotMunicipalites.ListerMunicipalitiesActives();
        stats.NombreMunicipalitesImportees = municipalitesImportees.Count();

        HashSet<int> codesExistantsActifs = new(this.depotMunicipalites.ListerMunicipalitiesActives()
            .Select(m => m.Code)
        );

        HashSet<int> codesImportees = new(
            municipalitesImportees
                .Select(m => m.Code)
        );

        foreach (Municipalite m in municipalitesImportees)
        {
            if (codesExistantsActifs.Contains(m.Code))
            {
                stats.NombreMunicipalitesNonModifiees++;
            }
            else if (this.depotMunicipalites.ChercherMunicipaliteParCode(m.Code) == null)
            {
                this.depotMunicipalites.AjouterMunicipalite(m);
                stats.NombreMunicipalitesAjoute++;
            }
            else
            {
                this.depotMunicipalites.MajMunicipalite(m);
                stats.NombreMunicipalitesMisesAJour++;
            }
        }

        foreach (Municipalite m in municipalitesActives)
        {
            if (!codesImportees.Contains(m.Code))
            {
                this.depotMunicipalites.DesactiverMunicipalite(m.Code);
                stats.NombreMunicipalitesDesactives++;
            }
        }

        return stats;
    }
}