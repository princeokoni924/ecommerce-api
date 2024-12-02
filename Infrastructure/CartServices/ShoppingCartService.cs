using System.Text.Json;
using Core.Contract.ICartServices;
using Core.Entities.RedisShopCart;
using StackExchange.Redis;

namespace Infrastructure.CartServices
{

    public class ShoppingCartService(IConnectionMultiplexer redis) : IShoppingCartServices
    {    
        private readonly IDatabase _db = redis.GetDatabase();

        public async Task<bool> DeleteCartAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }

        public async Task<ShoppingCart?> GetShoppingCartAsync(string key)
        {
        var data = await _db.StringGetAsync(key);
         return data.IsNullOrEmpty ? null: JsonSerializer.Deserialize<ShoppingCart>(data!);
        }

          public async Task<ShoppingCart?> SetShoppingCartAsync(ShoppingCart cart)
        {
            var created = await _db.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));
            if(!created){
                return null;
            }else{
                return await GetShoppingCartAsync(cart.Id);
            }

        }
    }
}