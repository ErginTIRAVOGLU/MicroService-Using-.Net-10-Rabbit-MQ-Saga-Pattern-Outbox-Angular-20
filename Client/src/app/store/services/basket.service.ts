import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import type { Basket } from "../models/Basket";
import type { Observable } from "rxjs";



@Injectable({ providedIn: 'root' })
export class BasketService {
    private http = inject(HttpClient);

    // Gateway URL
    private baseUrl = 'http://localhost:8030/Basket';

    // Initialize from local storage
    private basketSignal = signal<Basket | null>(this.loadBasket());
    basketCount = signal<number>(this.calcTotalItems(this.basketSignal()));

    private loadBasket(): Basket | null {
        const stored = localStorage.getItem('basket');
        return stored ? JSON.parse(stored) : null;
    }

    private saveBasket(basket: Basket) {
        localStorage.setItem('basket', JSON.stringify(basket));
    }

    private calcTotalItems(basket: Basket | null): number {
        return basket ? basket.items.reduce((sum, i) => sum + i.quantity, 0) : 0;
    }

    getBasket(username:string):Observable<Basket>{
        return this.http.get<Basket>(`${this.baseUrl}/${username}`);
    }

    updateBasket(basket:Basket):Observable<Basket>{
        return this.http.post<Basket>(this.baseUrl,basket);
    }

    deleteBasket(username:string):Observable<any>{
        return this.http.delete(`${this.baseUrl}/${username}`);
    }

    setBasket(basket:Basket){
        this.basketSignal.set(basket);
        this.basketCount.set(this.calcTotalItems(basket));
        this.saveBasket(basket);
    }

    get basket(){
        return this.basketSignal();
    }

    clearBasket(){
        this.basketSignal.set(null);
        this.basketCount.set(0);
        localStorage.removeItem('basket');
    }

}