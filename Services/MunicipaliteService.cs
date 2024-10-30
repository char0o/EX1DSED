using DalMunicipaliteSQL;
using Entite;

namespace Services;

public class MunicipaliteService
{
    private readonly IDepotMunicipalites depotMunicipalites;

    public MunicipaliteService(IDepotMunicipalites depotMunicipalites)
    {
        this.depotMunicipalites = depotMunicipalites;
    }

    public IEnumerable<MunicipaliteDTO> ListerMunicipalitesActives()
    {
        var municipalites = depotMunicipalites.ListerMunicipalitiesActives();

        return municipalites.Select(c => new MunicipaliteDTO
        {
            Code = c.Code,
            Nom = c.Nom,
            Region = c.Region,
            SiteWeb = c.SiteWeb,
            DateElection = c.DateElection
        }).ToList();
    }

    public void AjouterMunicipalite(MunicipaliteDTO municipalite)
    {
        depotMunicipalites.AjouterMunicipalite(municipalite.VersEntite());
    }
}