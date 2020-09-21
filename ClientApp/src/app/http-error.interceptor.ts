import {
HttpEvent,
HttpInterceptor,
HttpHandler,
HttpRequest,
HttpResponse,
HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';

import { AuthenticationService } from './services/authentication.service'
@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private authenticationService: AuthenticationService){

  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  return next.handle(request)
    .pipe(
      retry(1),
      catchError((error: HttpErrorResponse) => {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
          // client-side error
          errorMessage = `Error: ${error.error.message}`;
        } else {
          // server-side error
          errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        if(error.status == 404){
          this.router.navigate(['404']);
        }

        if(error.status === 401) {
          this.authenticationService.logout();
          location.reload(true);
        }
        return throwError(errorMessage);
      })
    )
  }
}
