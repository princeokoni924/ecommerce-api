import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

loading = false; // setiing loading to fals
busyRequestCount = 0;
 busy (){
  
  this.busyRequestCount ++; // increase busy requst count by 1
  this.loading = true;
 }

 // idle method
 idle(){
  this.busyRequestCount --; //decrease request count by 1
  if(this.busyRequestCount <=0){
    this.busyRequestCount = 0;
    this.loading = false;
  }
 }
}
