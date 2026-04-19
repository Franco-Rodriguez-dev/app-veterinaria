import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Veterinaria } from '../interfaces/veterinaria';
import { VeterinariaDetalle } from '../interfaces/veterinaria-detalle';

@Injectable({
  providedIn: 'root'
})
export class VeterinariaService {

  private baseUrl = `${environment.apiUrl}/veterinaria`

  constructor(private http: HttpClient) {}

  // 🔹 Crear persona + mascota
  crearConMascota(data: Veterinaria): Observable<Veterinaria> {
    return this.http.post<Veterinaria>(`${this.baseUrl}/crearConMascota`, data);
  }

  // 🔹 Obtener listado general
  getListadoGeneral(): Observable<Veterinaria[]> {
    return this.http.get<Veterinaria[]>(`${this.baseUrl}/listadoGeneral`);
  }

  // 🔹 Eliminar persona + sus mascotas
  deleteConMascotas(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/eliminarConMascotas/${id}`);
  }

  // 🔹 Actualizar persona + mascota
  updateConMascota(id: number, data: Veterinaria): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/actualizarConMascota/${id}`, data);
  }

  getPorId(id: number | string): Observable<Veterinaria> {
  return this.http.get<Veterinaria>(`${this.baseUrl}/${id}`);
}

getDetalle(id: number | string): Observable<VeterinariaDetalle> {
  return this.http.get<VeterinariaDetalle>(`${this.baseUrl}/${id}`);
}

}
