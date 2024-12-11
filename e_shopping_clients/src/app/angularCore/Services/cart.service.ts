import { computed, inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cart, CartItem } from '../../shared/models/Cart';
import { Product } from '../../shared/models/Product';
import { environment } from '../../../environments/environment';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
baseUrl = environment.apiUrl;
private http = inject(HttpClient)
cartSignal = signal<Cart | null> (null);
// updating the cart count when new item is added
itemCount = computed(()=>{
  return this.cartSignal()?.items.reduce((sum, item)=> sum + item.quantity, 0) /** 
   *reduce the array of items into a number that is going to represent the item count
   **/
})
 

getCart(getCartById:string){
return this.http.get<Cart>(this.baseUrl + 'shoppingcart?id=' + getCartById).pipe(
 map(cartSignal =>{
  this.cartSignal.set(cartSignal);
  return cartSignal;
 })
);
}



setCart(setCart: Cart){
return this.http.post<Cart>(this. baseUrl+ 'shoppingcart', setCart).subscribe({
  next: setCartSub => this.cartSignal.set(setCartSub),
  
})
}


// add item to cart
addItemsToCart(item: CartItem |Product, quantity =1){
 const getAllCart = this.cartSignal()?? this.createCart();
 if(this.isProduct(item)){
  //map product to cart item``  
  item = this.mapProductToCartItem(item);
 }
 getAllCart.items = this.addOrUpdateItem(getAllCart.items, item, quantity,);
 this.setCart(getAllCart)
}
  addOrUpdateItem(items: CartItem[], item: CartItem, quantity: number): CartItem[] {
   const index = items.findIndex(z=>z.productId === item.productId);

   if(index === -1){
    item.quantity = quantity;
    items.push(item)
   }else{
    items[index].quantity +=quantity
   }
   return items
  }

  // private addOrUpdateItem(items: CartItem[], quantity: number, item:CartItem): CartItem[] {
  //   const index = items.findIndex(i=>i.productId === item.productId);
  //   if(index === -1){
  //     item.quantity = quantity;
  //     items.push(item)
  //   }else{
  //    items[index].quantity += quantity
  //   }
  //     return items
  // }


  




// map product to cart item
   private  mapProductToCartItem(item: Product): CartItem{
   return {
    productId: item.id,
    productName: item.name,
    price: item.price,
    quantity:0,
    pictureUrl: item.pictureUrl,
    brand: item.brand,
    type: item.type
   }
  }
 


private isProduct(item: CartItem | Product) : item is Product{
return (item as Product).id !== undefined
}



private createCart(): Cart {
    const CreatNewCart = new Cart();
    localStorage.setItem('cart_id', CreatNewCart.id);
    return CreatNewCart;
  }
}
