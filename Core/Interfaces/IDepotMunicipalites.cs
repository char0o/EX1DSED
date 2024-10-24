

using Core.Entities;

namespace Core.Interfaces
{
    public interface IDepotMunicipalites
    {
        Municipalite ChercherMunicipaliteParCode(int code);
        IEnumerable<Municipalite> ListerMunicipalitiesActives();
        void DesactiverMunicipalite(int code);
        void AjouterMunicipalite(Municipalite municipalite);
        void ModifierMunicipalite(Municipalite municipalite);
        IEnumerable<Municipalite> ListerMunicipalities();
    }
}