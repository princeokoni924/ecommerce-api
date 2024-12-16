import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../angularCore/Services/shop.service';
import { Product } from '../../shared/models/Product';
import { MatCard } from '@angular/material/card';
import { ProductItemComponent } from "./product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { FilterDialogComponent } from './filter-dialog/filter-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { ShopParams } from '../../shared/models/shopParams';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/Pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    MatCard,
    ProductItemComponent,
    MatButton, // for btn
    MatIcon, // for icon
    MatMenu, // for sorting
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    MatPaginator,
    FormsModule // for search engine
],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {

private shopService = inject(ShopService);
// inject dialog service
private shopDialogService = inject(MatDialog)
products?: Pagination<Product>;
sortOptions = [
  {name: "Alphabertical",value: "name"},
  {name:"Price: Low-High",value: 'priceAsc'},
  {name: 'Price: High-Low', value: 'priceDesc'}
]

// initalize obj for shop params
shopParams = new ShopParams();
pageSizeOptions= [5,10,15,20,25]



ngOnInit(): void {
  // invoke initializeShop here
  this.initializeShopService()

}


// method to initialize the shop
initializeShopService(){
  this.shopService.getBrands();
  this.shopService.getTypes();
  // invoke get method helper
  this.getProductHelper();
}

getProductHelper(){
  this.shopService.getProducts(this.shopParams).subscribe({
    next: response => this.products = response,
    error: error => alert(error)
    
  })
}
// method for search
onSearchChange(){
  this.shopParams.pageNumber = 1;
  this.getProductHelper();
}
handlePageEvent(event: PageEvent){
 this.shopParams.pageNumber = event.pageIndex + 1;
 this.shopParams.pageSize = event.pageSize;
 this.getProductHelper();
}
// invoking the onsortchange method
onSortChange(event: MatSelectionListChange){
 const selectedOption = event.options[0]
 if(selectedOption){
  this.shopParams.sort = selectedOption.value;
  this.shopParams.pageNumber = 1;
  this.getProductHelper();
  
 }
}

// method to open the dialog
openFilterDialog()
{
 const dialogRef = this.shopDialogService.open(FilterDialogComponent,{
  minWidth: '500px',
  data:{
     selectedBrands: this.shopParams.brands,
    selectedTypes: this.shopParams.types
  }
 });
dialogRef.afterClosed().subscribe
({
  next: result =>{
    if(result){
     // alert(result);
      this.shopParams.brands = result.selectedBrands;
      this.shopParams.types = result.selectedTypes;
      this.shopParams.pageNumber = 1;
     // invoke get product helper method here
     this.getProductHelper();
    }
  }
})
}
}
