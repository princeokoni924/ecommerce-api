using System;
using Core.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext (DbContextOptions opts): DbContext(opts)
{
 public DbSet<Product> Products {get; set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfigurations).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
