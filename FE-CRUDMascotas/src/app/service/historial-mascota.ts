import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  HistorialMascota,
  HistorialMascotaCreate,
  HistorialMascotaUpdate,
  TipoHistorialMascota
} from '../interfaces/historial-mascota';

@Injectable({
  providedIn: 'root'
})
export class HistorialMascotaService {
  private baseUrl = `${environment.apiUrl}/HistorialMascota`;

  constructor(private http: HttpClient) {}

  getTipos(): Observable<TipoHistorialMascota[]> {
    return this.http.get<TipoHistorialMascota[]>(`${this.baseUrl}/tipos`);
  }

  getByMascota(mascotaId: number | string): Observable<HistorialMascota[]> {
    return this.http.get<HistorialMascota[]>(`${this.baseUrl}/mascota/${mascotaId}`);
  }

  getById(id: number | string): Observable<HistorialMascota> {
    return this.http.get<HistorialMascota>(`${this.baseUrl}/${id}`);
  }

  create(data: HistorialMascotaCreate): Observable<HistorialMascota> {
    return this.http.post<HistorialMascota>(this.baseUrl, data);
  }

  update(id: number | string, data: HistorialMascotaUpdate): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, data);
  }

  delete(id: number | string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
