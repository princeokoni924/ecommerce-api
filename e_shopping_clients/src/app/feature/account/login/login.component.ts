import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AccountService } from '../../../angularCore/Services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    
    MatCard,
    MatFormField,
    MatLabel,
    MatInput,
    MatButton
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
private formBuilder = inject(FormBuilder);
private accountServices = inject(AccountService);
private router = inject(Router);


// login form
loginForm = this.formBuilder.group({
  email: [' '],
  password: [' ']
});

// submit form
onSubmit(){
  this.accountServices.login(this.loginForm.value).subscribe({
    next: ()=>{
      // if user has successfully log-in, get the user infor
      this.accountServices.getUserInfor().subscribe();
      //and navigate the user to the shop after logging in
      this.router.navigateByUrl('/shop')
    },
    error: (err)=> alert('error getting user info')
  })
}
}
