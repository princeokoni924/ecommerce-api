import { Component, HostListener, inject, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { ShopComponent } from "./feature/shop/shop.component";
import { HttpClient } from '@angular/common/http';
import { Cart } from './shared/models/Cart';
import { Pagination } from './shared/models/Pagination';
import { Product } from './shared/models/Product';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
     HeaderComponent,
      ShopComponent
    ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export  class AppComponent  implements OnInit{
 
  baseUrl = 'https://localhost:5073/api/'
private http = inject(HttpClient)
  title = 'ShopMax';
  products : Product[]=[]

  ngOnInit(): void {
    this.http.get<Pagination<Product>>(this.baseUrl + 'product').subscribe({
      next: response => this.products = response.data,
      error: (error) => alert(error),
      complete: ()=> alert('completed')
      
    })
  }
   
    // public getScreenWidth: any;
    // public getScreenHeight: any;

    // ngOnInit(){
    //   this.getScreenWidth = window.innerWidth;
    //   this.getScreenHeight = window.innerHeight;
    // }

    // @HostListener('window: resize', ['$event'])
    // OnWindowResize(){
    //   this.getScreenWidth = window.innerWidth;
    //   this.getScreenHeight = window.innerHeight;
    // }
  }

