using Core.DTOs;
using Core.Entities;
using Core.Interfaces;

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
        IEnumerable<Municipalite> municipalites = depotMunicipalites.ListerMunicipalitiesActives();
        
        return municipalites.Select( c => new MunicipaliteDTO()
        {
            Code = c.Code,
            Nom = c.Nom,
            Region = c.Region,
            SiteWeb = c.SiteWeb,
            DateElection = c.DateElection,
            Actif = c.Actif
        }).ToList();
    }

    public void AjouterMunicipalite(MunicipaliteDTO municipalite)
    {
        depotMunicipalites.AjouterMunicipalite(municipalite.VersEntite());
    }
}