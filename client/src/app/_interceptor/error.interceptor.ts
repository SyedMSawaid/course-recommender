import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {NavigationExtras, Router} from '@angular/router';
import {catchError} from 'rxjs/operators';
import {ToastrService} from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modelStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modelStateErrors.push(error.error.errors[key]);
                  }
                }
                for (const x of modelStateErrors) {
                  if ('description' in x) {
                    this.toastr.error(x.description);
                  } else {
                    this.toastr.error(x);
                  }
                }
              }
              else {
                this.toastr.error(error.error, error.status);
              }
              break;

            case 401:
              this.toastr.error(error.error, error.status);
              break;

            default:
              this.toastr.error('Something unexpected gone wrong.');
              console.log(error);
              break;
          }
        }
        return throwError(error);
      })
    );
  }
}
