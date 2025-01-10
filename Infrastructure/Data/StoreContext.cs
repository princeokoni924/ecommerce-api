using System;
using Core.Entities;
using Infrastructure.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext (DbContextOptions opts): IdentityDbContext<ShopUser>(opts)
{
 public DbSet<Product>? Products {get; set;}
public DbSet<Address>? Addresses {get; set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfigurations).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
