import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Mascota } from '../interfaces/mascota';

@Injectable({
  providedIn: 'root' // 👉 Hace que el servicio esté disponible en toda la app
})
export class MascotaService {//esto es lo que se injecta

  private baseUrl = `${environment.apiUrl}/mascota`
  
  // 👉 URL base de la API, tomada del archivo environment (ej: https://localhost:5001/)
// private myAppUrl: string = environment.endpoint;

  // 👉 Ruta específica del controlador Mascota en tu backend ASP.NET (ej: api/Mascota)
 //private myApiUrl: string = 'api/Mascota/';

 //private baseUrl: string = `${environment.apiUrl}/Mascota`; tambien se puede hacer de esta manera 
//y en metodo get usamos esto return this.http.get<Mascota>(`${this.baseUrl}/${id}`);

  // 👉 HttpClient permite hacer llamadas HTTP (GET, POST, PUT, DELETE) al backend
  constructor(private http: HttpClient) {}

  // 👉 Método para obtener mascotas (GET)
  // Retorna un Observable, que el componente deberá suscribirse para obtener los datos
  getMascota(): Observable<Mascota[]> {
    // 👉 Combina la URL base y la ruta API -> (ej: https://localhost:5001/api/Mascota)
    return this.http.get<Mascota[]>(this.baseUrl);
  }

  getMascotasVer(id : string | number) : Observable<Mascota>{
    return this.http.get<Mascota>(`${this.baseUrl}/${id}`);
  }

  deleteMascota(id : string | number) : Observable<void>{
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  addMascota(mascota : Mascota) : Observable<Mascota>{
    return this.http.post<Mascota>(this.baseUrl,mascota);
  }


  updateMascota(id : string | number , mascota : Mascota) : Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`,mascota)
  }

}
