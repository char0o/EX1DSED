using Core.Interfaces;
using Data;
using Data.Depots;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DalMunicipaliteSQL;
using DepotImportationMunicipalitesCSV;
using DepotImportationMunicipalitesJSON;

namespace Program
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
            
            string? connectionString = config.GetConnectionString("DBMunicipalite");
            
            services.AddSingleton<IConfiguration>(config);
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IDepotImportationMunicipalites, DepotImportationMunicipalitesCSV.DepotImportationMunicipalitesCSV>();
            services.AddScoped<IDepotImportationMunicipalites, DepotImportationsMunicipalitesJSON>();
            services.AddScoped<IDepotMunicipalites, DepotMunicipalite>();
            
            services.AddScoped<StatistiquesImportation>();
            services.AddScoped<ImportationFichier>();

            
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            
            ImportationFichier importationFichier = serviceProvider.GetService<ImportationFichier>();
            StatistiquesImportation stats = serviceProvider.GetService<StatistiquesImportation>();
            
            importationFichier.TraiterDepotCSV();
            Console.WriteLine(stats.ToString());
        }
    }
}
