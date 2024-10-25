using Core;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Data;
using Data.Db;
using Data.Repos;
using Services;

namespace DSED_M01_Fichiers_Texte
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    "Server=.;Database=BD_Municipalite;TrustServerCertificate=True;Integrated Security=True;");
            });

            services.AddScoped<IDepotMunicipalites, DepotMunicipalite>();
            services.AddScoped<IDepotCSV, DepotCSV>();
            services.AddSingleton<StatistiquesImportation>();
            services.AddScoped<MunicipaliteService>();
            services.AddScoped<TraitementCSV>();

            var serviceProvider = services.BuildServiceProvider();

            var traitementCSV = serviceProvider.GetRequiredService<TraitementCSV>();
            var stats = serviceProvider.GetRequiredService<StatistiquesImportation>();

            traitementCSV.TraiterDepotCSV();
            Console.WriteLine(stats.ToString());
            traitementCSV.TraiterDepotCSV();
            Console.WriteLine(stats.ToString());
        }
    }
}
