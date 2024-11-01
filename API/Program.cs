using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Infrastructure.ServicConfig;
using Infrastructure.Data.SeedData;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddDbContext<StoreContext>(opt=>{
// opt.UseSqlServer(builder.Configuration.GetConnectionString("StoreConns"));
// });
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

// config seedData here
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
