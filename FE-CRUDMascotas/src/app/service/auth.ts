import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { LoginResponse } from '../interfaces/login-response';
import { Observable, BehaviorSubject } from 'rxjs';
import { LoginRequest } from '../interfaces/login-request';

@Injectable({
  providedIn: 'root'
})
export class Auth {

  private baseUrl = `${environment.apiUrl}/Auth`;

  // 🔥 estado reactivo
  private loggedIn = new BehaviorSubject<boolean>(!!localStorage.getItem('token'));
  isLoggedIn$ = this.loggedIn.asObservable();

  constructor(private http: HttpClient) {}

  login(data: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.baseUrl}/login`, data);
  }

  // 🔥 logout PRO
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
    localStorage.removeItem('rol');
    this.loggedIn.next(false); // 👈 notifica a toda la app
  }

  // 🔥 guardar sesión PRO
  saveSession(res: LoginResponse) {
    localStorage.setItem('token', res.token);
    localStorage.setItem('usuario', res.usuario);
    localStorage.setItem('rol', res.rol);
    this.loggedIn.next(true); // 👈 notifica login
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRol(): string | null {
    return localStorage.getItem('rol');
  }

  // 🔹 lo dejamos (sirve para guards o checks rápidos)
  isLogged(): boolean {
    return !!localStorage.getItem('token');
  }
}