import { HttpClient } from "@angular/common/http";
import { inject, Injectable} from "@angular/core";
import type { Observable } from "rxjs";
import type { CatalogResponse } from "../models/CatalogResponse";
import type { Brand } from "../models/Brand";
import type { Type } from "../models/Type";
import type { Product } from "../models/Product";


@Injectable({ providedIn: 'root' })
export class ProductService {
    private http = inject(HttpClient);

    // Gateway URL
    private baseUrl = 'http://localhost:8030/Catalog';

    getAllProducts(
        page: number,
        size: number,
        brand?: string | null,
        type?: string | null,
        sort?: string | null,
        search?: string | null
    ): Observable<CatalogResponse> {
        let params: string[] = [`pageIndex=${page}`, `pageSize=${size}`];

        if (brand) params.push(`brand=${encodeURIComponent(brand)}`);
        if (type) params.push(`type=${encodeURIComponent(type)}`);
        if (sort && sort !== 'default') params.push(`sort=${encodeURIComponent(sort)}`);
        if (search) params.push(`search=${encodeURIComponent(search)}`);

        return this.http.get<CatalogResponse>(
            `${this.baseUrl}/Products?${params.join('&')}`
        );
    }

    getAllBrands(): Observable<Brand[]> {
        return this.http.get<Brand[]>(`${this.baseUrl}/Brands`);
    }

    getAllTypes(): Observable<Type[]> {
        return this.http.get<Type[]>(`${this.baseUrl}/Types`);
    }

    GetProductById(id: string): Observable<Product> {
        return this.http.get<Product>(`${this.baseUrl}/Products/${id}`);
    }
}