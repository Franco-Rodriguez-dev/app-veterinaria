import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  ClienteMascotaUsuarioCreate,
  ClienteMascotaUsuarioResponse
} from '../interfaces/cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private baseUrl = `${environment.apiUrl}/Cliente`;

  constructor(private http: HttpClient) {}

  crearUsuarioConMascota(data: ClienteMascotaUsuarioCreate): Observable<ClienteMascotaUsuarioResponse> {
    return this.http.post<ClienteMascotaUsuarioResponse>(
      `${this.baseUrl}/crear-usuario-con-mascota`,
      data
    );
  }
}
