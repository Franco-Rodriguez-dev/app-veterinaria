export interface persona{
    id?: number;
  nombre: string;
  apellido: string;
  edad: number;
  sexo: string;
  telefono: string;
  cantMascotas?: number;
}

export interface PersonaDetalle {
  id: number;
  nombre: string;
  apellido: string;
  edad: number;
  sexo: string;
  telefono: string;
  mascotas: MascotaMini[];
}

export interface MascotaMini {
  id: number;
  nombre: string;
  raza: string;
}