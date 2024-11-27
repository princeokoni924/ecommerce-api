import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../../angularCore/Services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../shared/models/Product';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [],
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
