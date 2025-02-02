using System.ComponentModel.DataAnnotations;

namespace Core.Redis
{
    public class ShoppingCart
    {
        public required string Id { get; set; }
        
        [Required]
        public List<CartItem> Items { get; set; }= [];
        public int? DeliveryMethodId { get; set; }
        public object? BuyerEmail { get; set; }
        public object? BuyerPhone { get; set; }
        public object? BuyerName { get; set; }

          public string? ClientSecretCode { get; set; }
        public string? PaymentIntentId {get; set;}
    }

    
}