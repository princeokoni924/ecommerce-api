using Core.Contract.ICartServices;
using Core.Entities.RedisShopCart;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ShoppingCartController(IShoppingCartServices _cartService):BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById(string id){
           var cart = await _cartService.GetShoppingCartAsync(id);
           // if there's already an obj in the cart return the cart, if it null then create new cart
           return Ok(cart ?? new ShoppingCart{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> EditCart(ShoppingCart shoppingCart){
          var editCart = await _cartService.SetShoppingCartAsync(shoppingCart);
          if(editCart == null){
            return BadRequest("Ooops"+", "+"error occure trying to update the  cart");
          }else{
            return Ok();
          }
        }
          
        [HttpDelete]
        public async Task<ActionResult> DeleteCart(string id){
            var result = await _cartService.DeleteCartAsync(id);
            if(!result){
                return BadRequest("sorry"+", "+" error occure why trying to delete the item from the cart");
            }else{
                return Ok("item deleted successfully");
            }
        }
    }
}