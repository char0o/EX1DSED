using Data;
using Entite;
using Moq;
// ReSharper disable All

namespace ImportationFichierTests;

public class ImportationFichierTests
{
    [Fact]
    public void ImportationDuFichierDevraitAjouterAuDepotToutesLesMunicipalites()
    {
        Mock<IDepotImportationMunicipalites> depotImportation = new Mock<IDepotImportationMunicipalites>();
        Mock<IDepotMunicipalites> depotMunicipalite = new Mock<IDepotMunicipalites>();
        
        List<Municipalite> municipalitesAImportees = new List<Municipalite>
        {
            new Municipalite(123, "Granby", "Estrie", "www.com", null),
            new Municipalite(432, "Quebec", "Estrie", "www.com", null),
            new Municipalite(6252, "Shawinigan", "Estrie", "www.com", null),
            new Municipalite(7645, "Montreal", "Estrie", "www.com", null),
            new Municipalite(3263, "Amos", "Estrie", "www.com", null),
        };

        depotImportation.Setup(depot => depot.ImporterMunicipalites()).Returns(municipalitesAImportees);
        
        ImportationFichier importationFichier = new ImportationFichier(depotMunicipalite.Object, depotImportation.Object);

        importationFichier.TraiterFichier();

        foreach (Municipalite m in municipalitesAImportees)
        {
            depotMunicipalite.Verify(d => d.AjouterMunicipalite(m), Times.Once());
        }
    }
    
    [Fact]
    public void AjouterMunicipaliteDevraitEtreAppele1FoisParMunicipalitePlusieursImportations()
    {
        Mock<IDepotImportationMunicipalites> depotImportation = new Mock<IDepotImportationMunicipalites>();
        Mock<IDepotMunicipalites> depotMunicipalite = new Mock<IDepotMunicipalites>();
        
        List<Municipalite> municipalitesAImportees = new List<Municipalite>
        {
            new Municipalite(123, "Granby", "Estrie", "www.com", null),
            new Municipalite(432, "Quebec", "Estrie", "www.com", null),
            new Municipalite(6252, "Shawinigan", "Estrie", "www.com", null),
            new Municipalite(7645, "Montreal", "Estrie", "www.com", null),
            new Municipalite(3263, "Amos", "Estrie", "www.com", null),
        };
        
        List<Municipalite> municipalites = new List<Municipalite>();
        depotMunicipalite.Setup(depot => depot.ListerMunicipalitiesActives()).Returns(municipalites);
        depotMunicipalite.Setup(depot => depot.AjouterMunicipalite(It.IsAny<Municipalite>()))
            .Callback<Municipalite>(m => municipalites.Add(m));
        
        depotImportation.Setup(depot => depot.ImporterMunicipalites()).Returns(municipalitesAImportees);
        
        ImportationFichier importationFichier = new ImportationFichier(depotMunicipalite.Object, depotImportation.Object);

        importationFichier.TraiterFichier();
        importationFichier.TraiterFichier();
        
        foreach (Municipalite m in municipalitesAImportees)
        {
            depotMunicipalite.Verify(d => d.AjouterMunicipalite(m), Times.Once());
        }
    }
    
    [Fact]
    public void ImportationDuFichierDevraitDesactiveToutesLesMunicipalites()
    {
        Mock<IDepotImportationMunicipalites> depotImportation = new Mock<IDepotImportationMunicipalites>();
        Mock<IDepotMunicipalites> depotMunicipalite = new Mock<IDepotMunicipalites>();
        
        List<Municipalite> municipalitesAImportees = new List<Municipalite>
        {
            new Municipalite(123, "Granby", "Estrie", "www.com", null),
            new Municipalite(432, "Quebec", "Estrie", "www.com", null),
            new Municipalite(6252, "Shawinigan", "Estrie", "www.com", null),
            new Municipalite(7645, "Montreal", "Estrie", "www.com", null),
            new Municipalite(3263, "Amos", "Estrie", "www.com", null),
        };

        depotImportation.Setup(depot => depot.ImporterMunicipalites()).Returns(new List<Municipalite>());
        depotMunicipalite.Setup(depot => depot.ListerMunicipalitiesActives()).Returns(municipalitesAImportees);
        
        ImportationFichier importationFichier = new ImportationFichier(depotMunicipalite.Object, depotImportation.Object);

        importationFichier.TraiterFichier();
        
        foreach (Municipalite m in municipalitesAImportees)
        {
            depotMunicipalite.Verify(d => d.DesactiverMunicipalite(m.Code), Times.Once());
        }
    }
    
    [Fact]
    public void ImportationDuFichierDevraitMajToutesLesMunicipalites()
    {
        Mock<IDepotImportationMunicipalites> depotImportation = new Mock<IDepotImportationMunicipalites>();
        Mock<IDepotMunicipalites> depotMunicipalite = new Mock<IDepotMunicipalites>();
        
        List<Municipalite> municipalitesAImportees = new List<Municipalite>
        {
            new Municipalite(123, "Granby", "Estrie", "www.com", null),
            new Municipalite(432, "Quebec", "Estrie", "www.com", null),
            new Municipalite(6252, "Shawinigan", "Estrie", "www.com", null),
            new Municipalite(7645, "Montreal", "Estrie", "www.com", null),
            new Municipalite(3263, "Amos", "Estrie", "www.com", null),
        };

        depotImportation.Setup(depot => depot.ImporterMunicipalites()).Returns(new List<Municipalite>());
        depotMunicipalite.Setup(depot => depot.ListerMunicipalitiesActives()).Returns(municipalitesAImportees);
        
        ImportationFichier importationFichier = new ImportationFichier(depotMunicipalite.Object, depotImportation.Object);

        importationFichier.TraiterFichier();
        
        foreach (Municipalite m in municipalitesAImportees)
        {
            depotMunicipalite.Verify(d => d.DesactiverMunicipalite(m.Code), Times.Once());
        }
    }
}