import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import type { CheckoutPayload } from "../models/CheckoutPayload";
import type { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class CheckoutService {
    private http = inject(HttpClient);

    // Gateway URL
    private baseUrl = 'http://localhost:8030/Basket';

    checkout(payload: CheckoutPayload): Observable<any> {
        const token = localStorage.getItem('token');
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${token}`
        })
        return this.http.post(`${this.baseUrl}/Checkout`, payload,{headers});
    }

}