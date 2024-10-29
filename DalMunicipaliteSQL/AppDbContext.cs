using DalMunicipaliteSQL.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DalMunicipaliteSQL
{
    public class AppDbContext : DbContext
    {
        public DbSet<MunicipaliteDTO> Municipalite { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MunicipaliteDTO>().ToTable("municipalite");

            base.OnModelCreating(modelBuilder);
        }
    }
}
