using Core.DTOs;

namespace Core.Interfaces
{
    public interface IDepotImportationMunicipalites
    {
        IEnumerable<MunicipaliteDTO> ImporterMunicipalites();
    }
}
