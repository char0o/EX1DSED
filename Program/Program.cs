using Data;
using DependencyInjection;
using Entite;
using Microsoft.Extensions.DependencyInjection;

namespace ImportationFichiers;

internal class Program
{
    private static void Main(string[] args)
    {
        ServiceProvider sp = DependencyInjectionConfig.Instance;
        ImportationFichier importationFichier = sp.GetRequiredService<ImportationFichier>();

        StatistiquesImportation stats = importationFichier.TraiterFichier();
        Console.WriteLine(stats.ToString());
    }
}