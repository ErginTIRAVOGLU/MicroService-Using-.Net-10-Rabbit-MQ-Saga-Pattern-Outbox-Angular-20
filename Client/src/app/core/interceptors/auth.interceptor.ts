import type { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { throwError } from "rxjs";
import type { Observable } from "rxjs/internal/Observable";
import { catchError } from "rxjs/internal/operators/catchError";


@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    private router = inject(Router);


    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError((error:HttpErrorResponse) => {
                if (error.status === 401) {
                    this.router.navigate(['/unauthenticated']);
                }
                return throwError(() => error);
            })
        );
    }
}