using Core.Contract;
using Core.Contract.ICartServices;
using Core.Contract.IGeneric;
using Infrastructure.Data;
using Infrastructure.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.CartServices;
using Infrastructure.Services;

namespace Infrastructure.ServicConfig;
public static class ServicConfiguration
{
 public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
 {
  
    // Add service containers
  services.AddDbContext<StoreContext>(opt=>opt.UseSqlServer(configuration.GetConnectionString("StoreConns")));
  services.AddScoped<IProductRepository,ProductRepository>();
  services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
  services.AddSingleton<IShoppingCartServices, ShoppingCartService>();
  services.AddScoped<IPaymentService, PaymentService>();

  
  
  
      return services;
 }
}
