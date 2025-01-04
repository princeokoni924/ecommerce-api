import { Component, inject, input } from '@angular/core';
import { CartService } from '../../../angularCore/Services/cart.service';
import { Cart_Item } from '../../../shared/models/Cart';
import { RouterLink } from '@angular/router';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [
    RouterLink,
    MatButton,
    MatIcon,
    CurrencyPipe
  ],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss'
})
export class CartItemComponent {
// using signal 
item = input.required<Cart_Item>();
cartService = inject(CartService);

incrementQuantity(){
  this.cartService.addItemsToCart(this.item());
}

decrementQuantity(){
  this.cartService.removeItemFromCart(this.item().productId);
}

removeItem(){
  this.cartService.removeItemFromCart(this.item().productId, this.item().quantity);
}
}
