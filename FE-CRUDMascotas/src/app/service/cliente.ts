import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  ClienteMascotaUsuarioCreate,
  ClienteMascotaUsuarioResponse,
  MiPerfilCliente
} from '../interfaces/cliente';
import { Mascota } from '../interfaces/mascota';

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

  getMiPerfil(): Observable<MiPerfilCliente> {
    return this.http.get<MiPerfilCliente>(`${this.baseUrl}/mi-perfil`);
  }

  getMiMascota(mascotaId: string | number): Observable<Mascota> {
    return this.http.get<Mascota>(`${this.baseUrl}/mi-mascota/${mascotaId}`);
  }
}
