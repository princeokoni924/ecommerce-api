using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Infrastructure.ServicConfig;
using Infrastructure.Data.SeedData;
using API.MiddleWare;
using StackExchange.Redis;
using Core.Entities;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<ShopUser>()
.AddEntityFrameworkStores<StoreContext>();
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>{
  var connString = builder.Configuration.GetConnectionString("Redis");
  if(connString == null){
    throw new Exception("sorry"+", "+"something went wrong!"+" "+"maybe we couldn't fetch data from redis");
  }
  var configuration = ConfigurationOptions.Parse(connString, true);
   return ConnectionMultiplexer.Connect(configuration);
});


// adding cors service
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }


/*
configure middleware here
*/
app.UseMiddleware<ExceptionMiddleWare>();
// configuring cors service
/*
servise to allow are, (1) allow header,
 allow any method,
  from a specific origin
*/
app.UseCors(
  c=>c.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
.WithOrigins("http://localhost:4200","https://localhost:4200"));

//app.UseHttpsRedirection();

// app.UseAuthentication();
//  app.UseAuthorization();



app.MapControllers();
app.MapGroup("api").MapIdentityApi<ShopUser>();

// config  seedData and the http Request pipline here 
try{
using var createScope = app.Services.CreateScope();
var serviceProvider = createScope.ServiceProvider;
var getRequiredServicesContext = serviceProvider.GetRequiredService<StoreContext>();
 await getRequiredServicesContext.Database.MigrateAsync();
 await StoreContextSeedData.SeedDataAsync(getRequiredServicesContext);
}catch(Exception){
Console.WriteLine("error occured while seeding data");
throw;
}

app.Run();
