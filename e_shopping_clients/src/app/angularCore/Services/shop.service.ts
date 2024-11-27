import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Pagination } from '../../shared/models/Pagination';
import { Product } from '../../shared/models/Product';
import { ShopParams } from '../../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5073/api/';
  private http = inject(HttpClient);
  // get type and brand here
  types: string[] = [];
  brands: string[] =[];

  getProducts(shopParams: ShopParams)
  {
    //let message: string
    let params = new HttpParams();
    if(shopParams.brands.length>0)
    {
      params = params.append('brands', shopParams.brands.join(','))
    }

    if(shopParams.types.length>0)
    {
      params = params.append('types', shopParams.types.join(','))
    }
     
    if( shopParams.sort){
      params = params.append('sort', shopParams.sort )
    }

    if(shopParams.Search){
      params = params.append('search', shopParams.Search)
    }else{
    
    }
    params = params.append('pageSize', shopParams.pageSize);
    params = params.append('pageIndex', shopParams.pageNumber)

   return this.http.get<Pagination<Product>>(this.baseUrl+ 'product', {params})
  }

  getProductById(id: number){
   return this.http.get<Product>(this.baseUrl + 'product/' + id);
  }

  // method to get the brands
   getBrands(){
    if(this.brands.length > 0)
     return;

    return this.http.get<string[]>(this.baseUrl + 'product/brand').subscribe
    ({
        next: brandResponse => this.brands = brandResponse
    })
   }

   // method to get type
   getTypes(){
    if(this.types.length>0)
      return;
    return this.http.get<string[]>(this.baseUrl + 'product/type').subscribe
    ({
      next: typeResponse => this.types = typeResponse
    })
   }
}
