import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpHandler,
  HttpEvent,
  HttpRequest,
  HttpErrorResponse,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        if (error instanceof HttpErrorResponse) {
          const applicationError = error.headers.get('Application-Error');

          if (applicationError) {
            console.error(applicationError);
            return throwError(applicationError);
          }
          ///ModelState Errors
          const serverError = error.error;
          let modelStateErrors = '';
          if (serverError && typeof serverError === 'object') {
            for (const key in serverError) {
                if(key=='errors' && typeof serverError[key] === 'object'){
                  for(const key2 in serverError[key]){
                    if(serverError[key][key2]){
                      modelStateErrors += serverError[key][key2] + '\n';
                    }
                  }
                }
              // if (serverError[key] ) {
              //   modelStateErrors += serverError[key] + '\n';
              // }
            }
          }
          //Unauthorized errors
          if (error.status === 401) {
            return throwError(error.statusText);
          }
          //why before implement unauthorized errors the 'Server Error' did not appear in the console
          return throwError(modelStateErrors || serverError || 'Server Error');
        }
        return throwError('');
      })
    );
  }
}
export const ErrorInterceptorProvidor = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true,
};
