import type { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, throwError, type Observable } from "rxjs";


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    private router = inject(Router);


    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError((error:HttpErrorResponse) => {
                if (error.status === 500) {
                    this.router.navigate(['/server-error']);
                }
                return throwError(() => error);
            })
        );
    }
}
