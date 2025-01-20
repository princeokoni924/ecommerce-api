import { CanActivateFn, Router } from '@angular/router';
import { CartService } from '../Services/cart.service';
import { SnackbarService } from '../Services/snackbar.service';
import { inject } from '@angular/core';

export const emptyCartGuardGuard: CanActivateFn = (route, state) => {
  const cartService = inject(CartService);
  const router = inject(Router);
  const snackBarService = inject(SnackbarService)
  

  if(!cartService.cart() || cartService.cart()?.items.length === 0){
    snackBarService.error('sorry!'+', '+'you have no item in your cart');
    router.navigateByUrl('/cart');
    return false;
  }
  return true;
};
