using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Municipalite> Municipalite { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipalite>().ToTable("municipalite");

            base.OnModelCreating(modelBuilder);
        }
    }
}
