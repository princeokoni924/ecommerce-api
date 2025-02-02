import { Component, inject, OnInit } from '@angular/core';
import { OrderSummaryComponent } from "../../share/components/order-summary/order-summary.component";
import { MatStepperModule } from '@angular/material/stepper';
import { RouterLink } from '@angular/router';
import { MatButton } from '@angular/material/button';
import { StripeService } from '../../angularCore/servics/stripe.service';
import { StripeAddressElement } from '@stripe/stripe-js';
import { SnackbarService } from '../../angularCore/Services/snackbar.service';
@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [
    OrderSummaryComponent,
    MatStepperModule,
    RouterLink,
    MatButton
  ],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss'
})
export class CheckoutComponent implements OnInit {
private stripeService = inject(StripeService);
addressElements?: StripeAddressElement;
private snackBarService = inject(SnackbarService)



async ngOnInit() {
 
  try {
    this.addressElements = await this.stripeService.createStripeAddressElements();
    this.addressElements.mount('#address-element');
  } catch (error: any) {
    this.snackBarService.error('Error loading address elements');
  }
}
}
