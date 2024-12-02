namespace Core.Entities.RedisShopCart
{
    public class ShoppingCart
    {
        public required string Id { get; set; }

        public List<CartItems> Items { get; set; }=[];
    }

    
}