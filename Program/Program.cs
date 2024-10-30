using Data;
using DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ImportationFichiers;

internal class Program
{
    private static void Main(string[] args)
    {
        var sp = DependencyInjectionConfig.Instance;
        var importationFichier = sp.GetRequiredService<ImportationFichier>();

        var stats = importationFichier.TraiterFichier();
        Console.WriteLine(stats.ToString());
    }
}