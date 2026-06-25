import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { RestablecerPasswordRequest } from '../interfaces/restablecer-password';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private baseUrl = `${environment.apiUrl}/Usuario`;

  constructor(private http: HttpClient) {}

  restablecerPassword(data: RestablecerPasswordRequest): Observable<string> {
    return this.http.put(`${this.baseUrl}/restablecer-password`, data, {
      responseType: 'text'
    });
  }
}
