import type { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { finalize } from "rxjs";
import type { Observable } from "rxjs/internal/Observable";
import { LoadingService } from "../services/loading.service";

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
    private loadingService = inject(LoadingService);


    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.loadingService.show();
        return next.handle(req).pipe(
            finalize(() => this.loadingService.hide())
        );
    }
}