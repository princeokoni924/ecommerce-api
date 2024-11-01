using System;
using Core.Contract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServicConfig;

public static class ServicConfiguration
{
 public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
 {
  services.AddDbContext<StoreContext>(opt=>opt.UseSqlServer(configuration.GetConnectionString("StoreConns")));
  services.AddScoped<IProductRepository, ProductRepository>();
      return services;
 }
}
