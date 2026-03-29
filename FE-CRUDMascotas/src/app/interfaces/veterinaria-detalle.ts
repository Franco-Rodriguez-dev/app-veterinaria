export interface VeterinariaDetalle {
    // Datos de Persona
 nombre: string;
  apellido: string;
  telefono: string;
  edad: number;
  sexo: string;

  mascota: {
    nombre: string;
    raza: string;
    color: string;
    edad: number;
    peso: number;
  };
  
}
