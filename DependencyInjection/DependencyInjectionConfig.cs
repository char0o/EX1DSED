using DalMunicipaliteSQL;
using Data;
using DepotImportationMunicipalitesCSV;
using DepotImportationMunicipalitesJSON;
using Entite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection;

public class DependencyInjectionConfig
{
    private static readonly Lazy<ServiceProvider> _serviceProvider = new Lazy<ServiceProvider>(() =>
    {
        ServiceCollection services = new ServiceCollection();

        IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true);

        IConfigurationRoot config = builder.Build();

        string? connectionString = config.GetConnectionString("BDMunicipalite");

        services.AddSingleton<IConfiguration>(config);
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<DepotImportationMunicipalitesCsv>();
        services.AddScoped<DepotImportationsMunicipalitesJson>();
        services.AddScoped<IDepotMunicipalites, DepotMunicipalite>();

        services.AddScoped<ImportationFichier>(provider =>
        {
            string? fileType = config.GetSection("ImportFileType").Value;
            if (fileType == null)
            {
                throw new ArgumentNullException("FileType not found in JSON file");
            }

            IDepotMunicipalites depotMunicipalites = provider.GetRequiredService<IDepotMunicipalites>();

            switch (fileType)
            {
                case "CSV":
                {
                    IDepotImportationMunicipalites import =
                        provider.GetRequiredService<DepotImportationMunicipalitesCsv>();
                    return new ImportationFichier(depotMunicipalites, import);
                }
                case "JSON":
                {
                    IDepotImportationMunicipalites import =
                        provider.GetRequiredService<DepotImportationsMunicipalitesJson>();
                    return new ImportationFichier(depotMunicipalites, import);
                }
                default:
                {
                    throw new ArgumentException("File type not supported");
                }
            }
        });

        return services.BuildServiceProvider();
    });

    public static ServiceProvider Instance => _serviceProvider.Value;
}