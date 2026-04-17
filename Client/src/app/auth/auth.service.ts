import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import type { Observable } from "rxjs";
import type { RegisterDto } from "./models/RegisterDto";
import type { LoginDto } from "./models/LoginDto";
import {jwtDecode} from "jwt-decode";
import { JwtPayload } from "./models/JwtPayload";

@Injectable({ providedIn: 'root' })
export class AuthService {
    private http = inject(HttpClient)
    private baseUrl = 'http://localhost:8030/Auth';

    userToken = signal<string | null>(localStorage.getItem('token'));

    register(dto: RegisterDto): Observable<{ message: string }> {
        return this.http.post<{ message: string }>(`${this.baseUrl}/register`, dto);
    }

    login(dto: LoginDto): Observable<{ token: string }> {
        return this.http.post<{ token: string }>(`${this.baseUrl}/login`, dto);
    }

    saveToken(token: string) {
        localStorage.setItem('token', token);
        this.userToken.set(token);
    }

    logout() {
        localStorage.removeItem('token');
        this.userToken.set(null);
    }

    isLoggedIn(): boolean {
        return !!localStorage.getItem('token');
    }

    // get username > extract name/email from JWT
    getUserName(): string | null {
        const token = this.userToken();
        if (!token) return null;

        try {
            const decoded = jwtDecode<JwtPayload>(token);
            return decoded.name || decoded.sub || null;
        } catch (error) {
            console.error(error)
            return null;
        }
    }
}