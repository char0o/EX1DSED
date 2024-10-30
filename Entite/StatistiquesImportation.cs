namespace Entite;

public class StatistiquesImportation
{
    public int NombreMunicipalitesImportees { get; set; }
    public int NombreMunicipalitesMisesAJour { get; set; }
    public int NombreMunicipalitesNonModifiees { get; set; }
    public int NombreMunicipalitesDesactives { get; set; }
    public int NombreMunicipalitesAjoute { get; set; }

    public override string ToString()
    {
        return $"Nombre de municipalités importées : {this.NombreMunicipalitesImportees}\n" +
               $"Nombre de municipalités mises à jour : {this.NombreMunicipalitesMisesAJour}\n" +
               $"Nombre de municipalités non modifiées : {this.NombreMunicipalitesNonModifiees}\n" +
               $"Nombre de municipalités désactivées : {this.NombreMunicipalitesDesactives}\n" +
               $"Nombre de municipalités ajoutées : {this.NombreMunicipalitesAjoute}";
    }
}