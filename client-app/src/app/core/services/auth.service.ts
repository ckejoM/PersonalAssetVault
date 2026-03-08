import { Injectable, signal, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';
import { Router } from '@angular/router';

// Match the DTOs from our .NET backend
export interface LoginRequest { email: string; password: string; }
export interface AuthResponse { token: string; user: any; }

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);

  // Hardcode for local testing. In production, this comes from environment.ts
  private apiUrl = 'https://localhost:7128/api/auth'; 

  // Modern Angular Signal for reactive UI state
  public isAuthenticated = signal<boolean>(this.hasToken());

  login(credentials: LoginRequest) {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
        this.isAuthenticated.set(true);
        this.router.navigate(['/dashboard']);
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.isAuthenticated.set(false);
    this.router.navigate(['/login']);
  }

  private hasToken(): boolean {
    return !!localStorage.getItem('token');
  }
}