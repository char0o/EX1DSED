using Entite;

namespace DalMunicipaliteSQL;

public class DepotMunicipalite : IDepotMunicipalites
{
    private readonly AppDbContext appDbContext;

    public DepotMunicipalite(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public Municipalite? ChercherMunicipaliteParCode(int code)
    {
        MunicipaliteDTO? municipalite = this.appDbContext.Municipalite.SingleOrDefault(x => x.Code == code);
        return municipalite == null ? null : municipalite.VersEntite();
    }

    public IEnumerable<Municipalite> ListerMunicipalitiesActives()
    {
        return this.appDbContext.Municipalite.Where(c => c.Actif == true)
            .Select(c => c.VersEntite());
    }

    public void DesactiverMunicipalite(int code)
    {
        MunicipaliteDTO? municipalite = this.appDbContext
            .Municipalite
            .SingleOrDefault(x => x.Code == code);

        if (municipalite != null)
        {
            municipalite.Actif = false;
            this.appDbContext.SaveChanges();
        }
    }

    public void AjouterMunicipalite(Municipalite municipalite)
    {
        this.appDbContext.Municipalite.Add(new MunicipaliteDTO
        {
            Code = municipalite.Code,
            Nom = municipalite.Nom,
            DateElection = municipalite.DateElection,
            Region = municipalite.Region,
            SiteWeb = municipalite.SiteWeb,
            Actif = false
        });

        this.appDbContext.SaveChanges();
    }

    public void MajMunicipalite(Municipalite municipalite)
    {
        MunicipaliteDTO? municipaliteDTO = this.appDbContext.Municipalite
            .SingleOrDefault(x => x.Code == municipalite.Code);

        if (municipaliteDTO is null)
        {
            throw new ArgumentException("Aucun dto dans le bd avec ce code");
        }

        municipaliteDTO.Actif = true;

        this.appDbContext.SaveChanges();
    }
}