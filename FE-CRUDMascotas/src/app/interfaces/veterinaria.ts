export interface Veterinaria {
    
  personaId: number;
  nombre: string;
  apellido: string;
  telefono: string;
  mascotaId: number;
  nombreMascota: string;
  raza: string;
  peso: number;
}

export interface ClienteInactivo {
  personaId: number;
  nombre: string;
  apellido: string;
  telefono: string;
  username: string;
  cantidadMascotas: number;
}


