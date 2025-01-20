import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AccountService } from '../../../angularCore/Services/account.service';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { SnackbarService } from '../../../angularCore/Services/snackbar.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    
    MatCard,
    MatFormField,
    MatLabel,
    MatInput,
    MatButton,
    RouterLink
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
private formBuilder = inject(FormBuilder);
 accountServices = inject(AccountService);
private router = inject(Router);
private snackbar = inject(SnackbarService);
private activatedRoute = inject(ActivatedRoute)
returnUrl = '/shop';

constructor(){
 const url = this.activatedRoute.snapshot.queryParams['returnUrl'];
 if(url){
  this.returnUrl = url;
 }
}


// login form
loginForm = this.formBuilder.group({
  email: [' ', [Validators.required, Validators.email]],
  password: [' ', Validators.required]
});

// submit form
onSubmit(){
  if(this.loginForm.invalid){
    this.snackbar.error('login Colunms cannot be plank')
   }

  this.accountServices.login(this.loginForm.value).subscribe({
    next: ()=>{
      // if user has successfully log-in, get the user infor
      this.accountServices.getUserInfor().subscribe();
      //and navigate the user to the shop after logging in
      this.router.navigateByUrl(this.returnUrl)
    },
    error: (err) =>{
      this.snackbar.error('incorrect creedentials,kindly check and try again, thank you!')
    }
  })
}
}
