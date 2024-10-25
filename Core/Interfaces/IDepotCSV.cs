using Core.Entities;

namespace Data
{
    public interface IDepotCSV
    {
        IEnumerable<Municipalite> ImporterCSV(string cheminFichier);
    } 
}
