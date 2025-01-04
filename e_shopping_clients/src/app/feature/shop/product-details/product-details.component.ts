import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../../angularCore/Services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../shared/models/Product';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatFormField,MatFormFieldModule,MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';
import { CartService } from '../../../angularCore/Services/cart.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [
    CurrencyPipe,
    MatButton,
    MatIcon,
    MatFormField,
    MatInput,
    MatLabel,
    MatDivider,
    FormsModule
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  private shopService = inject(ShopService);
  private activatedRout = inject(ActivatedRoute);
  private cartService = inject(CartService);
  product?:Product;
quantityInCart = 0;
quantity = 1;

 ngOnInit(): void {
   this.loadProduct();
 }

 loadProduct(){
  const id = this.activatedRout.snapshot.paramMap.get('id')
  if(!id){return;} 
  this.shopService.getProductById(+id).subscribe({
    next:(product) => {
      this.product = product;
       // call update quantity in cart method in get product by id method
      this.updateQuantityInCart();
    },
    error: (error) => alert('error loading the store')
  });
 }


 // update quantity in cart
 updateQuantityInCart(){
    this.quantityInCart = this.cartService.cart()?.
    items.find(item => item
      .productId === this.product?.id)?.
      quantity || 0;

      this.quantity = this.quantityInCart || 1;
 }
//update cart
updateCart(){
  if(!this.product){
    return;
  }
  if(this.quantity > this.quantityInCart){
    const ItemToAdd = this.quantity - this.quantityInCart;
    this.quantityInCart += ItemToAdd;
    this.cartService.addItemsToCart(this.product, ItemToAdd);
  }else{
    const ItemToRemoveFromCart = this.quantityInCart - this.quantity;
    this.quantityInCart -= ItemToRemoveFromCart;
    this.cartService.removeItemFromCart(this.product.id, ItemToRemoveFromCart);
  }
}
 // get button text
  getButtonText(){
    return this.quantityInCart > 0 ? 'Update Cart' : 'Add to Cart';
  }
}
