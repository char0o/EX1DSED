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
        var stats = new StatistiquesImportation();

        var municipalitesImportees = depotImportation.ImporterMunicipalites();
        var municipalitesActives = depotMunicipalites.ListerMunicipalitiesActives();
        stats.NombreMunicipalitesImportees = municipalitesImportees.Count();

        var codesExistantsActifs = new HashSet<int>(
            depotMunicipalites.ListerMunicipalitiesActives()
                .Select(m => m.Code)
        );

        var codesImportees = new HashSet<int>(
            municipalitesImportees
                .Select(m => m.Code)
        );

        foreach (var m in municipalitesImportees)
            if (codesExistantsActifs.Contains(m.Code))
            {
                stats.NombreMunicipalitesNonModifiees++;
            }
            else if (depotMunicipalites.ChercherMunicipaliteParCode(m.Code) == null)
            {
                depotMunicipalites.AjouterMunicipalite(m);
                stats.NombreMunicipalitesAjoute++;
            }
            else
            {
                depotMunicipalites.MajMunicipalite(m);
                stats.NombreMunicipalitesMisesAJour++;
            }

        foreach (var m in municipalitesActives)
            if (!codesImportees.Contains(m.Code))
            {
                depotMunicipalites.DesactiverMunicipalite(m.Code);
                stats.NombreMunicipalitesDesactives++;
            }

        return stats;
    }
}