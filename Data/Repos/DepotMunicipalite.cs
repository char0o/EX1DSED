using Core.Entities;
using Core.Interfaces;
using Data.Db;

namespace Data.Repos
{
    public class DepotMunicipalite : IDepotMunicipalites
    {
        private readonly AppDbContext appDbContext;

        public DepotMunicipalite(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Municipalite? ChercherMunicipaliteParCode(int code)
        {
            return this.appDbContext.Municipalite.FirstOrDefault(x => x.Code == code);
        }

        public IEnumerable<Municipalite> ListerMunicipalitiesActives()
        {
            return appDbContext.Municipalite.Where(c => c.Actif == true);
        }

        public void DesactiverMunicipalite(int code)
        {
            Municipalite? municipalite = ChercherMunicipaliteParCode(code);

            if (municipalite != null)
            {
                municipalite.Actif = false;
                this.appDbContext.SaveChanges();
            }
        }

        public void AjouterMunicipalite(Municipalite municipalite)
        {
            appDbContext.Municipalite.Add(municipalite);
            appDbContext.SaveChanges();
        }

        public void ModifierMunicipalite(Municipalite municipalite)
        {

        }

        public IEnumerable<Municipalite> ListerMunicipalities()
        {
            return appDbContext.Municipalite;
        }
    }
}
