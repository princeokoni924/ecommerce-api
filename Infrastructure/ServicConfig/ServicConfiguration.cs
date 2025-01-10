using Core.Contract;
using Core.Contract.ICartServices;
using Core.Contract.IGeneric;
using Infrastructure.Data;
using Infrastructure.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.CartServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>{
  options.TokenValidationParameters = new TokenValidationParameters{
ValidateIssuer = true,
ValidateAudience = true,
ValidateLifetime = true,
ValidateIssuerSigningKey = true,
ValidIssuer = configuration["Jwt:Issuer"],
ValidAudience = configuration["Jwt:Audience"],
IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
  };
});
  // services.AddSingleton<IConnectionMultiplexer>(config=>{
  //   var connString = builder.configuration.GetConnectionString("Redis");
  //   if(connString == null){
  //     throw new Exception("cannot get redis connection string");
  //   }
  //  var configuration = ConfigurationOptions.Parse(connString, true);
  //  return ConnectionMultiplexer.Connect(configuration);
  // });
  
  
      return services;
 }
}
