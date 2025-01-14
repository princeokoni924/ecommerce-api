import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { forkJoin, of } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  // inject cart service
private cartService = inject(CartService);
private accountService = inject(AccountService)

// initialize init function to hold the cart id and observable
init(){
  //get cart id
 const cartId = localStorage.getItem('cart_id');

 // initializing observable. if cart is not found the return the cart id or if the cart is not exist return null
 const cart$ = cartId ? this.cartService.getCart(cartId) : of (null);
 
 // returning forkJoin. ForkJoin wait for multiple observable to complete, and then
 // it emit their latest values as and array 
 return forkJoin({
  cart: cart$,
  user: this. accountService.getUserInfor()
 })
}
}
