import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ClienteInactivo, Veterinaria } from '../interfaces/veterinaria';
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

  getClientesInactivos(): Observable<ClienteInactivo[]> {
    return this.http.get<ClienteInactivo[]>(`${this.baseUrl}/clientes-inactivos`);
  }

  reactivarCliente(personaId: number): Observable<string> {
    return this.http.put(`${this.baseUrl}/reactivar-cliente/${personaId}`, null, {
      responseType: 'text'
    });
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
