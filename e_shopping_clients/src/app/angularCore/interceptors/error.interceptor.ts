import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { SnackbarService } from '../Services/snackbar.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const snackbar = inject(SnackbarService)

  return next(req).pipe(catchError((error:HttpErrorResponse) =>{
  if(error.status == 400){
    const modelStateError = [];
    for (const key in error.error.error){
      if(error.error.errors[key]){
        modelStateError.push(error.error.errors[key])
      }
    }
    throw modelStateError.flat();
  }else{
    snackbar.error(error.error.title || error.error)
  }

  if(error.status == 401){
    snackbar.error(error.error.title || error.error)
  }

  if(error.status == 404){
    router.navigateByUrl('/not-found')
  }

  if(error.status == 500){
    const navigationExtras: NavigationExtras = {
      state: {err:error.error}
    }
    router.navigateByUrl('/server-error', navigationExtras)
  }
  return throwError(()=> error)
  }
  ));
};
