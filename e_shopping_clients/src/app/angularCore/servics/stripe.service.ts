import { inject, Injectable } from '@angular/core';
import {loadStripe, Stripe, StripeAddressElement, StripeAddressElementOptions, StripeElements} from '@stripe/stripe-js';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CartService } from '../Services/cart.service';
import { Cart } from '../../shared/models/Cart';
import { firstValueFrom, map } from 'rxjs';
import { AccountService } from '../Services/account.service';
@Injectable({
  providedIn: 'root'
})
export class StripeService {
// create a stripe instance
private stripePromise:Promise<Stripe | null>;
private http = inject(HttpClient);
baseUrl = environment.apiUrl;
cartService = inject(CartService);
accountService = inject(AccountService);

// intialize stripe elements to loade stripe
private elements?: StripeElements;
private addressElements?: StripeAddressElement;

// initialize stripe promises to loade stripe
constructor() {
  this.stripePromise = loadStripe(environment.stripePublicKey);
}

// get stripe instance
getStripInstance(){
 return this.stripePromise;
}
// initialize stripe elements
async initializeStripeElements(){
  // if the elements is not exist then create new instance elements
  if(!this.elements){
  const stripe = await this.getStripInstance();
  if(stripe){
    // if stripe already exist create or update payment intent
    const createOrUpdateCartPaymentIntent = await firstValueFrom(this.createOrUpdatePaymentIntent());
    this.elements = stripe.elements({clientSecret: createOrUpdateCartPaymentIntent.clientSecret, appearance:{labels:'floating'}});
  }else{
    // if stripe is not exist throw an error
    throw new Error('Error lodding stripe properties');
  }
  }
  // return stripe elements
  return this.elements;
}

// initialize stripe address elements
async createStripeAddressElements(){
  // if the address elements is not exist then create new instance address elements
  if(!this.addressElements){
     const addressElements = await this.initializeStripeElements();
     if(addressElements){
      const user = this.accountService.currentUser();
      let defaultValues: StripeAddressElementOptions['defaultValues'] = {};
      if(user){
        defaultValues.name = user.firstName + ' ' + user.lastName;
      }

      // if(user?.address){
      //   defaultValues.address ={
      //     state: user.address.state,
      //     city: user.address.city,
      //     country: user.address.country,
      //     near
      //   }
      // }
      const options: StripeAddressElementOptions ={
        mode: 'shipping'
      };
      this.addressElements = addressElements.create('address', options);
     }else{
      throw new Error('error lodding address elements instance');
     }
  }
  // return address elements
  return this.addressElements;
}
// create or update payment intent
 createOrUpdatePaymentIntent(){
// get cart signal
const cartSignal = this.cartService.cart();
if(!cartSignal){
  throw new Error('Error lodding cart properties');
}else{
  return this.http.post<Cart>(this.baseUrl + 'payment/' + cartSignal.id, {}).pipe(
    map(mappingCartService =>{
      this.cartService.cart.set(mappingCartService);
      return mappingCartService;
    })
  )
}
}
}
