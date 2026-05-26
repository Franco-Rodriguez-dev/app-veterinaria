export type TipoHistorialMascota =
  | 'Consulta'
  | 'Servicio'
  | 'Medicamento'
  | 'Observacion'
  | 'Vacuna'
  | 'BanoYCorte'
  | 'Control'
  | 'Otro';

export interface HistorialMascota {
  id: number;
  mascotaId: number;
  fecha: string;
  tipo: TipoHistorialMascota;
  titulo: string;
  descripcion: string;
  observaciones?: string;
  precio?: number;
  proximaVisita?: string;
  creadoPorUsuarioId?: number;
  fechaCreacion: string;
  activo: boolean;
}

export interface HistorialMascotaCreate {
  mascotaId: number;
  fecha: string;
  tipo: TipoHistorialMascota;
  titulo: string;
  descripcion: string;
  observaciones?: string;
  precio?: number | null;
  proximaVisita?: string | null;
}

export interface HistorialMascotaUpdate {
  fecha: string;
  tipo: TipoHistorialMascota;
  titulo: string;
  descripcion: string;
  observaciones?: string;
  precio?: number | null;
  proximaVisita?: string | null;
}
