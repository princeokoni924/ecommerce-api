import { Component, inject } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatButton } from '@angular/material/button';
import{MatIcon} from '@angular/material/icon'
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { BusyService } from '../../angularCore/Services/busy.service';
import { MatProgressSpinner, MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBar, MatProgressBarModule } from '@angular/material/progress-bar';
import { MatPaginatorModule } from '@angular/material/paginator';
import { CartService } from '../../angularCore/Services/cart.service';
import { AccountService } from '../../angularCore/Services/account.service';
import { MatMenu, MatMenuItem, MatMenuTrigger } from '@angular/material/menu';
import { MatDivider } from '@angular/material/divider';
import { MatList, MatListItem } from '@angular/material/list';


@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatIcon,
    MatButton,
    MatBadge,
    RouterLink,
    RouterLinkActive,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,
  MatMenuTrigger,
 MatMenu,
MatDivider,
MatMenuItem
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
// [x: string]: any;
busyService = inject(BusyService)
cartService = inject(CartService)
// invoke accout service to populate a notification when user login
accountService = inject(AccountService);
// router will redirect the user to some where else when the logout
private router = inject(Router);

logout(){
  
  this.accountService.logout().subscribe({
    next: ()=>{
      // update user to null when they logout
      this.accountService.currentUser.set(null);
       // redirect to home page
       this.router.navigateByUrl('/')
    },
    complete: ()=>{
      message: 'successfully logout '
    }
  })
 
  
}


}
