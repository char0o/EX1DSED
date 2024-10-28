using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Data;
using Data.Db;
using Data.Repos;
using Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace DSED_M01_Fichiers_Texte
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            
            IConfigurationBuilder? builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            
            IConfigurationRoot config = builder.Build();
            
            string? connectionString = config.GetConnectionString("BDMunicipalite");
            string? csvFilePath = config.GetSection("DepotSettings")["CSVFilePath"];
            string? jsonPath = config.GetSection("DepotSettings")["JSONPath"];
            
            services.AddSingleton<IConfiguration>(config);
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IDepotImportationMunicipalites, DepotCSV>(provider => new DepotCSV(csvFilePath));
            //services.AddScoped<IDepotImportationMunicipalites, DepotJSON>(provider => new DepotJSON(jsonPath));
            services.AddScoped<IDepotMunicipalites, DepotMunicipalite>();
            
            services.AddSingleton<StatistiquesImportation>();
            services.AddScoped<TraitementCSV>();

            
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            
            {
                TraitementCSV traitementCSV = serviceProvider.GetRequiredService<TraitementCSV>();
                StatistiquesImportation stats = serviceProvider.GetRequiredService<StatistiquesImportation>();
                traitementCSV.TraiterDepotCSV(); 
                Console.WriteLine(stats.ToString());
            }
            Console.ReadKey();
            {
                TraitementCSV traitementCSV = serviceProvider.GetRequiredService<TraitementCSV>();
                StatistiquesImportation stats = serviceProvider.GetRequiredService<StatistiquesImportation>();
                traitementCSV.TraiterDepotCSV();
                Console.WriteLine(stats.ToString());
            }

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                TraitementCSV traitementCSV = scope.ServiceProvider.GetRequiredService<TraitementCSV>();
                StatistiquesImportation stats = scope.ServiceProvider.GetRequiredService<StatistiquesImportation>();
                traitementCSV.TraiterDepotCSV();

                Console.WriteLine(stats.ToString());
            }
            
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                TraitementCSV traitementCSV = scope.ServiceProvider.GetRequiredService<TraitementCSV>();
                StatistiquesImportation stats = scope.ServiceProvider.GetRequiredService<StatistiquesImportation>();
                traitementCSV.TraiterDepotCSV();
                Console.WriteLine(stats.ToString());
            }
        }
    }
}
