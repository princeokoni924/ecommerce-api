import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { HttpClient } from '@angular/common/http';
import { products } from './shared/models/Product';
import { Pagination } from './shared/models/Pagination';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  // implemented
  baseUrl = 'https://localhost:5073/api/';
  // alternative injection
  private http = inject(HttpClient);
  title = 'ShopMax';
  //constructor(private http: HttpClient){}
  // display items on the browser
  products: products[] = [];

  // making http request
  ngOnInit(): void {
    // using this. keyword to get access to http
    this.http.get<Pagination<products>>(this.baseUrl + 'product').subscribe({
      // next: data => console.log(data), [check obj from the console]
      next: (response) => (this.products = response.data),

      error: (error) => console.log(error),
      complete: () => console.log('the program completed successfully'),
    });
  }
}
