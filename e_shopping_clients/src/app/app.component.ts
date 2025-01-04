import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { Component } from '@angular/core';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    
],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export  class AppComponent {
 
title = 'Shopmax'
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
  

