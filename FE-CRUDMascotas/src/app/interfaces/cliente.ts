export type Sexo = 'Masculino' | 'Femenino' | 'Otro';

export interface ClienteMascotaUsuarioCreate {
  nombre: string;
  apellido: string;
  edad: number;
  sexo: Sexo;
  telefono: string;
  username: string;
  password: string;
  nombreMascota: string;
  raza: string;
  color: string;
  edadMascota: number;
  peso: number;
}

export interface ClienteMascotaUsuarioResponse {
  personaId: number;
  usuarioId: number;
  mascotaId: number;
  username: string;
  nombreCompleto: string;
  nombreMascota: string;
}
