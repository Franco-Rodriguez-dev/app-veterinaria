import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { persona, PersonaDetalle } from '../interfaces/persona';

@Injectable({
  providedIn: 'root'
})
export class PersonaService {
  
  private baseUrl = `${environment.apiUrl}/persona`

  constructor (private http : HttpClient){}


  getPersonas(): Observable<persona[]> {
    return this.http.get<persona[]>(this.baseUrl);
  }

 getPersonaById(id : number): Observable<PersonaDetalle>{
    return this.http.get<PersonaDetalle>(`${this.baseUrl}/${id}`);
}
 
 createPersona(persona: Omit<persona, 'id' | 'cantMascotas'>): Observable <persona>{
  return this.http.post<persona>(this.baseUrl, persona);
 }

updatePersona(id: number , persona: Omit<persona, 'id' | 'cantidadMascota'>): Observable<void>{
  return this.http.put<void>(`${this.baseUrl}/${id}`, persona);
}

deletePersona(id: number): Observable<void>{
  return this.http.delete<void>(`${this.baseUrl}/${id}`);
}







}
