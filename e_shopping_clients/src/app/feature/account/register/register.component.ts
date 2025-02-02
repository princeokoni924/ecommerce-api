import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatError, MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AccountService } from '../../../angularCore/Services/account.service';
import { Router, RouterLink } from '@angular/router';
import { SnackbarService } from '../../../angularCore/Services/snackbar.service';
import { JsonPipe } from '@angular/common';
import { catchError } from 'rxjs';
import { TextInputComponent } from "../../../shared/components/text-input/text-input.component";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatCard,
    MatButton,
    TextInputComponent,
    RouterLink,
],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
private formBuilder = inject(FormBuilder);
private accountservice = inject(AccountService);
private router = inject(Router);
private snackBarService = inject(SnackbarService)
// validate error
validationErrors?: string[]

// registration instance
registerForm = this.formBuilder.group({
  firstName: ['', Validators.required],
  lastName: ['',Validators.required],
  email: ['',[Validators.required, Validators.email]],
  password: ['', Validators.required ], // password must contain at least 8 characters, one uppercase, one lowercase, one number and one special character
  confirmPassword:['', Validators.required],
  //address: ['address not provided']
});

// submit method
onSubmitRegister(){
  if(this.registerForm.invalid){
    this.snackBarService.error('sorry, you cannot leave the registration colunms blank')
  }
  this.accountservice.register(this.registerForm.value).subscribe({
    next: ()=>{
      this.snackBarService.success('Registration successful');
      this. router.navigateByUrl('/login');
    },
    error :error => this.validationErrors = error
  })
  }
}




