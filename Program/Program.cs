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
                    "Server=.;Database=BD_Municipalite;TrustServerCertificate=True;Integrated Security=True;MultipleActiveResultSets=True");
            });

            services.AddScoped<IDepotMunicipalites, DepotMunicipalite>();
            services.AddScoped<IImportCSV, ImportCSV>();
            services.AddScoped<StatistiquesImportation>();
            services.AddScoped<MunicipaliteService>();

            var serviceProvider = services.BuildServiceProvider();

            var csvImport = serviceProvider.GetRequiredService<IImportCSV>();
            
            csvImport.ImporterCsv("municipalites2.csv");
            csvImport.AjouterMunicipalites();
        }
    }
}
