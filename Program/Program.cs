using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DalMunicipaliteSQL;
using DepotImportationMunicipalitesJSON;
using DepotImportationMunicipalitesCSV;
using Entite;

namespace ImportationFichiers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            
            IConfigurationBuilder? builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            IConfigurationRoot config = builder.Build();
            
            string? connectionString = config.GetConnectionString("BDMunicipalite");
            
            services.AddSingleton<IConfiguration>(config);
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DepotImportationMunicipalitesCsv>();
            services.AddScoped<DepotImportationsMunicipalitesJson>();
            services.AddScoped<IDepotMunicipalites, DepotMunicipalite>();
            
            services.AddScoped<ImportationFichier>(provider =>
            {
                IConfiguration config = provider.GetRequiredService<IConfiguration>();
                String? fileType = config.GetSection("ImportFileType").Value;
                if (fileType == null)
                {
                    throw new ArgumentNullException("FileType not found in JSON file");
                }
                IDepotMunicipalites depotMunicipalites = provider.GetRequiredService<IDepotMunicipalites>();
                
                switch (fileType)
                {
                    case "CSV":
                    {
                        DepotImportationMunicipalitesCsv import =
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

            
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            
            ImportationFichier importationFichier = serviceProvider.GetService<ImportationFichier>();
            
            StatistiquesImportation stats = importationFichier.TraiterDepotCSV();
            Console.WriteLine(stats.ToString());
        }
    }
}
