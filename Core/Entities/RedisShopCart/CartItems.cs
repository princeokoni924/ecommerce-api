using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.RedisShopCart
{
    public class CartItems
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required string PictureUrl { get; set; }
        public required string Brand {get; set;}
        public required string ProductType {get; set;}
    }
}