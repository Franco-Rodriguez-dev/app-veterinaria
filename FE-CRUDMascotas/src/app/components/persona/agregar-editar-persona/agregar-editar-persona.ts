import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PersonaService } from '../../../service/persona';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MaterialModules } from '../../../shared/material';
import { Spinner } from '../../../shared/spinner/spinner';
import { NgIf } from '@angular/common';
import { persona } from '../../../interfaces/persona';

@Component({
  selector: 'app-agregar-editar-persona',
  standalone: true,
  imports: [NgIf,MaterialModules,Spinner,RouterModule],
  templateUrl: './agregar-editar-persona.html',
  styleUrls: ['./agregar-editar-persona.css']
})
export class AgregarEditarPersona implements OnInit {
 loading: boolean = false; // Controla el spinner de carga
  personaForm: FormGroup;   // Guarda la estructura y validaciones del formulario
  id: number;               // Guarda el id de la persona si se está editando
  operacion: string = 'Agregar'; // Texto que cambia entre "Agregar" o "Editar"


  constructor(
    private fb: FormBuilder,          // Construye el formulario reactivo
    private _personaService: PersonaService, // Servicio para hacer peticiones HTTP
    private _snackBar: MatSnackBar,   // Muestra mensajes flotantes de éxito/error
    private router: Router,           // Permite navegar entre rutas
    private aRoute: ActivatedRoute    // Permite leer parámetros de la URL (por ej: /editarPersona/3)
  ){

    //  DEFINIMOS LA ESTRUCTURA DEL FORMULARIO
    this.personaForm = this.fb.group({
      nombre: ['', Validators.required],
      apellido: ['', Validators.required],
      edad: ['', [Validators.required, Validators.min(1), Validators.max(100)]],
      telefono: ['', [Validators.required, Validators.pattern(/^(\+54|0)?[0-9]{8,15}$/)]],
      sexo: ['', Validators.required]
    });


    //  LEEMOS EL "id" DE LA RUTA (si viene)
    // Si la ruta es "/editarPersona/3", esto obtiene el 3
    this.id = Number(this.aRoute.snapshot.paramMap.get('id'));
}




  ngOnInit(): void {
    // Si el id es distinto de 0 significa que estamos editando, no agregando
    if (this.id !== 0) {
      this.operacion = 'Editar'; // Cambia el texto en pantalla
      this.obtenerPersona(this.id); // Carga los datos de la persona
    }
  }

   // --------------------------------------------------------
  // 🔹 OBTENER UNA PERSONA POR ID
  // --------------------------------------------------------
  obtenerPersona(id: number) {
    this.loading = true; // Muestra el spinner
    this._personaService.getPersonaById(id).subscribe(data => {
      // Carga los valores obtenidos en el formulario
      this.personaForm.patchValue({
        nombre: data.nombre,
        apellido: data.apellido,
        edad: data.edad,
        telefono: data.telefono,
        sexo: data.sexo
      });
      this.loading = false; // Oculta el spinner
    });
  }

  // --------------------------------------------------------
  // 🔹 MÉTODO PRINCIPAL (se ejecuta al presionar el botón “Guardar”)
  // --------------------------------------------------------
 agregarEditarPersona() {
  if (this.personaForm.invalid) {
    console.log("Formulario inválido");
    return;
  }

  const persona = {
    nombre: this.personaForm.value.nombre.trim(),
  apellido: this.personaForm.value.apellido.trim(),
  edad: this.personaForm.value.edad,
  telefono: this.personaForm.value.telefono.trim(),
  sexo: this.personaForm.value.sexo.trim()
  };

  console.log("Datos que se envían:", persona); // 👈 agregá esto para confirmar

  this._personaService.createPersona(persona).subscribe({
    next: (data) => {
      console.log("Persona creada:", data);
      this._snackBar.open('Persona agregada con éxito', '', { duration: 3000 });
      this.router.navigate(['/listadoPersona']);
    },
    error: (err) => {
      console.error("Error al crear persona:", err);
      this._snackBar.open('Error al crear la persona', '', { duration: 3000 });
    }
  });
}

  // --------------------------------------------------------
  // 🔹 EDITAR UNA PERSONA EXISTENTE
  // --------------------------------------------------------
  editarPersona(id: number, persona: persona) {
    this.loading = true;
    this._personaService.updatePersona(id, persona).subscribe(() => {
      this.loading = false;
      this.mensajeExito('actualizada');
      this.router.navigate(['/listadoPersona']); // Redirige al listado
    });
  }

  // --------------------------------------------------------
  // 🔹 AGREGAR UNA NUEVA PERSONA
  // --------------------------------------------------------
  agregarPersona(persona: persona) {
    this.loading = true;
    this._personaService.createPersona(persona).subscribe(() => {
      this.loading = false;
      this.mensajeExito('registrada');
      this.router.navigate(['/listadoPersona']); // Redirige al listado
    });
  }

  // --------------------------------------------------------
  // 🔹 MOSTRAR MENSAJE DE ÉXITO
  // --------------------------------------------------------
  mensajeExito(texto: string) {
    this._snackBar.open(`La persona fue ${texto} con éxito`, '', {
      duration: 4000, // Tiempo que dura el mensaje
      horizontalPosition: 'right' // Posición en pantalla
    });
  }
}