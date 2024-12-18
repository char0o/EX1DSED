﻿using Microsoft.EntityFrameworkCore;

namespace DalMunicipaliteSQL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<MunicipaliteDTO> Municipalite { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MunicipaliteDTO>().ToTable("municipalite");

        base.OnModelCreating(modelBuilder);
    }
}