using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IImportCSV
    {
        void ImporterCsv(string cheminFichier);
        void AjouterMunicipalites();
    } 
}
