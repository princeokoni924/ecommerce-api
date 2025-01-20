import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Address, User } from '../../shared/models/User';
import {catchError, map,tap, throwError} from 'rxjs'
//import {SignalService} from './signalr.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
baseUrl = environment.apiUrl
private http = inject(HttpClient);
currentUser = signal<User| null>(null);
//private signalServices = inject(this.signalServices);

login(data:any){
let params = new HttpParams();
params = params.append("useCookies", true);
// allow creedential when the user's login
return this.http.post<User>(this.baseUrl+'login', data,{params, withCredentials: true})
}

register(data: any){
return this.http.post(this.baseUrl + 'register', data).pipe(
catchError((err)=>{
  alert('Registeration fail');
  return throwError(()=> err)
})
)
}

getUserInfor(){
  // specify creedential to get user info
return this.http.get<User>(this .baseUrl+ 'account/user-info',{withCredentials:true}).pipe(
  map(user=>{
    this.currentUser.set(user);
    return user;

  })
)

}

logout(){
  // same creedential here
  return this.http.post(this.baseUrl+ 'account/logout', {}, {withCredentials: true})
}

updateUserAddress(address: Address){
return this.http.post(this.baseUrl+ 'account/ address', address).subscribe({
  
})
}

// Authentication service
getAuthState(){
  return this.http.get<{isAuthenticated: boolean}>(this.baseUrl + 'account/auth-status');
}
}
