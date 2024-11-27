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
    MatDivider
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  private shopService = inject(ShopService);
  private activatedRout = inject(ActivatedRoute);
  product?:Product;
 ngOnInit(): void {
   this.loadProduct();
 }

 loadProduct(){
  const id = this.activatedRout.snapshot.paramMap.get('id')
  if(!id){return;} 
  this.shopService.getProductById(+id).subscribe({
    next:(product) => this.product = product,
    error: (error) => alert(error)
  });
 }
}
