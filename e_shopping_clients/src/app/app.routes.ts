import { Routes } from '@angular/router';
import { HomeComponent } from './feature/home/home.component';
import { ShopComponent } from './feature/shop/shop.component';
import { ProductDetailsComponent } from './feature/shop/product-details/product-details.component';
import { ErrorHandlingComponent } from './feature/error-handling/error-handling.component';
import { CartComponent } from './feature/cart/cart.component';
import { CheckoutComponent } from './feature/checkout/checkout.component';

export const routes: Routes = [
 {path: '', component: HomeComponent},
 {path: 'shop', component: ShopComponent},
 {path: 'shop/:id', component: ProductDetailsComponent},
 {path:'cart', component:CartComponent},
 {path: 'checkout', component: CheckoutComponent},
 {path: 'error-handling', component: ErrorHandlingComponent},
 {path: 'server-error', component: ErrorHandlingComponent},
 {path: '**', redirectTo: 'not-found', pathMatch: 'full'}
];
