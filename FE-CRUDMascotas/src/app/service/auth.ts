
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { LoginResponse } from '../interfaces/login-response';
import { Observable } from 'rxjs';
import { LoginRequest } from '../interfaces/login-request';

@Injectable({
  providedIn: 'root'
})
export class Auth {

  private baseUrl = `${environment.apiUrl}/Auth`;

  constructor(private http: HttpClient) {}

  login(data: LoginRequest ): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.baseUrl}/login`, data);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
    localStorage.removeItem('rol');
  }

  saveSession(res: LoginResponse) {
    localStorage.setItem('token', res.token);
    localStorage.setItem('usuario', res.usuario);
    localStorage.setItem('rol', res.rol);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRol(): string | null {
    return localStorage.getItem('rol');
  }

  isLogged(): boolean {
    return !!localStorage.getItem('token');
  }
}