import { Component, HostListener} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { ShopComponent } from "./feature/shop/shop.component";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
     HeaderComponent,
      //ShopComponent
    ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export default class AppComponent {
    title = 'ShopMax'
    public getScreenWidth: any;
    public getScreenHeight: any;

    ngOnInit(){
      this.getScreenWidth = window.innerWidth;
      this.getScreenHeight = window.innerHeight;
    }

    @HostListener('window: resize', ['$event'])
    OnWindowResize(){
      this.getScreenWidth = window.innerWidth;
      this.getScreenHeight = window.innerHeight;
    }
  }

