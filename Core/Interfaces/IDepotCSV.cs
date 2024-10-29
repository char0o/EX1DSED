using Core.Entities;

namespace Core.Interfaces
{
    public interface IDepotCSV
    {
        IEnumerable<Municipalite> ImporterCSV(string cheminFichier);
    } 
}
