using Entite;

namespace DalMunicipaliteSQL
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
            MunicipaliteDTO? municipalite = this.appDbContext.Municipalite.FirstOrDefault(x => x.Code == code);
            return municipalite == null ? null : municipalite.VersEntite();
        }

        public IEnumerable<Municipalite> ListerMunicipalitiesActives()
        {
            return appDbContext.Municipalite.Where(c => c.Actif == true)
                .Select(c => c.VersEntite());
        }

        public void DesactiverMunicipalite(int code)
        {
            MunicipaliteDTO? municipalite = appDbContext
                .Municipalite
                .SingleOrDefault(x => x.Code == code);

            if (municipalite != null)
            {
                municipalite.Actif = false;
                appDbContext.SaveChanges();
            }
        }

        public void AjouterMunicipalite(Municipalite municipalite)
        {
            appDbContext.Municipalite.Add(new MunicipaliteDTO()
            {
                Code = municipalite.Code,
                Nom = municipalite.Nom,
                DateElection = municipalite.DateElection,
                Region = municipalite.Region,
                SiteWeb = municipalite.SiteWeb,
                Actif = false
            });
            appDbContext.SaveChanges();
        }

        public void ActiverMunicipalite(int code)
        {
            MunicipaliteDTO? municipalite = appDbContext
                .Municipalite
                .SingleOrDefault(x => x.Code == code);

            if (municipalite != null)
            {
                municipalite.Actif = true;
                appDbContext.SaveChanges();
            }
        }

        public IEnumerable<Municipalite> ListerMunicipalities()
        {
            return appDbContext.Municipalite.Select(m => m.VersEntite());
        }
    }
}
