import { APP_INITIALIZER, ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { errorInterceptor } from './angularCore/interceptors/error.interceptor';
import { loadingInterceptor } from './angularCor/interceptors/loading.interceptor';
import { InitService } from './angularCore/Services/init.service';
import { lastValueFrom } from 'rxjs';

// method the return the app initializer
function initializeApp(initService: InitService){
  // getting the whole of observable from the initservce
  return ()=> lastValueFrom(initService.init()).finally(()=>{
    const splash =document.getElementById('initial-splash');
    // check if there's anything in splash. if there's anything then call remove method to remove the node
    if(splash){
      splash.remove();
    }
  })
}

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true })
    , provideRouter(routes)
    , provideAnimationsAsync()
    ,provideHttpClient(withInterceptors([errorInterceptor, loadingInterceptor])),
    {
      provide: APP_INITIALIZER,
      // use initializer method here
      useFactory: initializeApp,
      multi: true,
      deps: [InitService] /** 
      * waiting for the functionality inside to load
       befor it can truely initialize the app
      */
       
    }
  ]
};
