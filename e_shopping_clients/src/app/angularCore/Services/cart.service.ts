import { computed, inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cart, Cart_Item } from '../../shared/models/Cart';
import { Product } from '../../shared/models/Product';
import { catchError, map, tap, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CartService {
baseUrl = environment .apiUrl;
private http = inject(HttpClient);

// set signal to hold the current cart
 cart = signal<Cart|null>(null);
// updating the cart count when new item is added and
// compute the signal to calculate the item count
itemCount = computed(()=>{ 
  const computedCartItems = this.cart();
  return computedCartItems?
  /**
   *reduce the array of items into a number that is going to represent the item count
   **/
  computedCartItems.items.reduce((sum, item)=>sum + item.quantity,0): 0;
});
 // compute the total item of the cart
 totalItems = computed(()=>{
  const itemInCart = this.cart();
  if(!itemInCart) return null;

  const subtotal = itemInCart.items.reduce((sum, item)=> sum + item.price * item.quantity, 0);
   const shipping = 3500; //itemInCart.shippingPrice;
   const discount = 0.04; //itemInCart.discount;

   return{
    subtotal,
    shipping,
    discount,
    total:subtotal + shipping - discount
   }
 })
// get cart by id
getCart(id: string){
return this.http.get<Cart>(this.baseUrl  +'cart?id=' + id).pipe(
 map(cart =>{
  this.cart.set(cart);
  return cart;
 })
);
}


// set item to cart
setCart(cart: Cart){
this.http.post<Cart>(this.baseUrl + 'cart', cart).subscribe({
  next: updatCart => this.cart.set(updatCart),
  error: err => alert('error trying to update cart')
})
}


// add item to cart
 addItemsToCart(item: Cart_Item | Product, quantity =1){
 const currentCart = this.cart()?? this.createCart();
 const newItem = this.isProduct(item)?  this.mapProductToCartItem(item) : item;

currentCart.items = this. addOrUpdateCartItem(currentCart.items, newItem, quantity);
this.setCart(currentCart);
 }

// remove item from cart
removeItemFromCart(productId:number, quantity = 1){
  const itemFromCart = this.cart();
  if(!itemFromCart)return;
  const itemIndex = itemFromCart.items.findIndex(z=>z.productId === productId);
  if(itemIndex !== -1){
    if(itemFromCart.items[itemIndex].quantity > quantity){
      itemFromCart.items[itemIndex].quantity-= quantity;
  }else{
    itemFromCart.items.splice(itemIndex, 1);
  }
  if(itemFromCart.items.length ==0){
    this.deleteCart();
}else{
  this.setCart(itemFromCart);
}
}
}
// delete cart
  deleteCart() {
    this.http.delete(this.baseUrl + 'cart?id=' + this.cart()?.id).subscribe({
     next: () => {
      localStorage.removeItem('cart_id');
      this.cart.set(null);
     }
     
    });
  }
  

 // add or update cart item
  private addOrUpdateCartItem(items: Cart_Item[], item: Cart_Item, quantity: number):Cart_Item[]{
    const index = items.findIndex(z=>z.productId === item.productId);
    if(index === -1){
      item.quantity = quantity;
      items.push(item)
    }else{
      items[index].quantity += quantity;
    }
    return items;
  }

  // map product to cart item
  private mapProductToCartItem(item: Product): Cart_Item{
    return {
      productId: item.id,
      productName: item.name,
      price: item.price,
      quantity: 0,
      pictureUrl: item.pictureUrl,
      brand: item.brand,
      productType: item.productType
    }
  }

  // check if item is product
  private isProduct(item: Cart_Item | Product): item is Product{
    return (item as Product).id !== undefined;
  }

  // create a new cart
  private createCart(): Cart{
    const newCart = new Cart();
    localStorage.setItem('cart_id', newCart.id);
    return newCart;
  }
}
