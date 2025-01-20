import { Component, inject } from '@angular/core';
import { CartService } from '../../angularCore/Services/cart.service';
import { CartItemComponent } from "./cart-item/cart-item.component";
import { OrderSummaryComponent } from "../../share/components/order-summary/order-summary.component";
import { EmptyStateComponent } from "../../shared/components/empty-state/empty-state.component";

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CartItemComponent,
    OrderSummaryComponent, EmptyStateComponent],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent {
cartService = inject(CartService);

}
