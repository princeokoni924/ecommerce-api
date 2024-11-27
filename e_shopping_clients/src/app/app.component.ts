import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { HttpClient } from '@angular/common/http';
import { Pagination } from './shared/models/Pagination';
import { Product} from './shared/models/Product'
import { ShopService } from './angularCore/Services/shop.service';
import { ShopComponent } from "./feature/shop/shop.component";


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
export class AppComponent{
    title = 'ShopMax'
  }

