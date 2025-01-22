using Core.Redis;

namespace Core.Contract.ICartServices
{
    public interface IShoppingCartServices
    {
        Task <ShoppingCart?> GetShoppingCartAsync(string key);
        Task <ShoppingCart?> SetShoppingCartAsync(ShoppingCart cart);
        Task<bool> DeleteCartAsync(string key);
    }
}