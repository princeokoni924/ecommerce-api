import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  // inject cart service
private cartService = inject(CartService);

// initialize init function to hold the cart id and observable
init(){
  //get cart id
 const cartId = localStorage.getItem('cart_id');

 // initializing observable. if cart is not found the return the cart id or if the cart is not exist return null
 const cartObservable$ = cartId? this.cartService.getCart(cartId) : of (null);
 
 // returning the observable
 return cartObservable$;
}
}
