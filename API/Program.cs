using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Infrastructure.ServicConfig;
using Infrastructure.Data.SeedData;
using API.MiddleWare;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServices(builder.Configuration);
// adding cors service
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();
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
app.UseCors(c=>c.AllowAnyHeader()
.AllowAnyMethod()
.WithOrigins("http://localhost:4200","https://localhost:4200"));

app.MapControllers();

// config  seedData and the http Request pipline here 
try{
using var createScope = app.Services.CreateScope();
var serviceProvider = createScope.ServiceProvider;
var getRequiredServicesContext = serviceProvider.GetRequiredService<StoreContext>();
 await getRequiredServicesContext.Database.MigrateAsync();
 await StoreContextSeedData.SeedDataAsync(getRequiredServicesContext);
}catch(Exception err){
Console.WriteLine(err);
throw;
}

app.Run();
