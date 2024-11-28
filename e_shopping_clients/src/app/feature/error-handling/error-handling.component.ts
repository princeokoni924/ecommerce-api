import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-error-handling',
  standalone: true,
  imports: [
    MatButton
  ],
  templateUrl: './error-handling.component.html',
  styleUrl: './error-handling.component.scss'
})
export class ErrorHandlingComponent {
baseUrl = 'https://localhost:5073/api/';
private http = inject(HttpClient)
validatError?: string[];

get404Error(){
  this.http.get(this.baseUrl+ 'buggy/notfound').subscribe({
    next: response => alert(response),
    error: (error) =>alert(error)
  })
}

get401Error(){
  this.http.get(this.baseUrl + 'buggy/badrequest').subscribe({
    next: response =>alert(response),
    error: (error)=>alert(error)
  })
}

get500Error(){
  this.http.get(this.baseUrl + 'buggy/internalerror').subscribe({
    next: (response) => alert(response),
    error: (error) => alert(error)
  })
}

get400ValidateError(){
this.http.get(this.baseUrl + 'buggy/validaterror').subscribe({
  next: (response)  =>alert(response),
  error: (error) => this.validatError = error
})
}
}
