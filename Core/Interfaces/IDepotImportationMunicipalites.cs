using Core.Entities;

namespace Core.Interfaces
{
    public interface IDepotImportationMunicipalites
    {
        IEnumerable<Municipalite> ImporterMunicipalites();
    }
}
