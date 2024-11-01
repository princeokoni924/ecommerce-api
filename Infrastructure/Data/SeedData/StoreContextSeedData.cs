using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;

namespace Infrastructure.Data.SeedData
{
    public class StoreContextSeedData
    {
        public static async Task SeedDataAsync(StoreContext storeSeed)
        {
           // if there's no product 
           if(!storeSeed.Products.Any())
           {
                // read the data from the json
                var readDataFromJon = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                // deserializing from json
                var deserializeProduct = JsonSerializer.Deserialize<List<Product>>(readDataFromJon);
                // if the serializer is null add product range
                if(deserializeProduct == null)
                    return;
                     storeSeed.Products.AddRange(deserializeProduct);
                     await storeSeed.SaveChangesAsync();
                
           }
        }
    }
}