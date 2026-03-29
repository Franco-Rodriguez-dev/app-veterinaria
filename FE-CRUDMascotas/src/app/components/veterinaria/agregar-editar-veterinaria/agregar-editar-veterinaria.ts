import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { VeterinariaService } from '../../../service/veterinaria';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MaterialModules } from '../../../shared/material';



@Component({
  selector: 'app-agregar-editar-veterinaria',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,MaterialModules],
  templateUrl: './agregar-editar-veterinaria.html',
  styleUrls: ['./agregar-editar-veterinaria.css']
})
export class AgregarEditarVeterinaria implements OnInit {
  loading : boolean = false;
  formVeterinaria : FormGroup;
  id : number =0;
  operacion : string = 'agregar';

  constructor(private fb: FormBuilder , private _veterinariaService: VeterinariaService,
   private _snackBar : MatSnackBar , private routes : Router , private aRoute : ActivatedRoute){

  this.formVeterinaria = this.fb.group({
    // Datos de la persona
      nombre: ['', Validators.required],
      apellido: ['', Validators.required],
      telefono: ['', Validators.required],
      edad: ['', [Validators.required, Validators.min(1)]],
      sexo: ['', Validators.required],
//el validators.required es para que si o si complete el campo si no , no se guarda el form
    mascota: this.fb.group({
      nombre: ['', Validators.required],
      raza: ['', Validators.required],
      color: ['', Validators.required],
      edad: ['', [Validators.required, Validators.min(0)]],
      peso: ['', [Validators.required, Validators.min(0)]],
    })

  })

  this.id =Number(this.aRoute.snapshot.paramMap.get('id'));

    
  }


 ngOnInit(): void {
    if (this.id !== 0) {
      this.operacion = 'Editar';
      this.cargarDatos();// TODO: traer datos para editar más adelante
    }
  }

  cargarDatos() {
  this.loading = true;
  this._veterinariaService.getPorId(this.id).subscribe({
    next: (data) => {
      this.formVeterinaria.patchValue(data);
      this.loading = false;
    },
    error: (err) => {
      console.error('Error al cargar datos:', err);
      this.loading = false;
      this._snackBar.open('No se pudieron cargar los datos', '', { duration: 3000 });
    }
  });
}


  agregarEditarVeterinaria() {
  if (this.formVeterinaria.invalid) return;

  const data= this.formVeterinaria.value;
  console.log('Datos enviados:', data);

  this.loading = true;

  // Si estás en modo EDITAR
  if (this.id !== 0) {
    this._veterinariaService.updateConMascota(this.id, data).subscribe({
      next: () => {
        this.loading = false;
        this._snackBar.open('Datos actualizados correctamente', '', {
          duration: 3000,
          horizontalPosition: 'right'
        });
        this.routes.navigate(['/listadoGeneral']); // vuelve al listado general
      },
      error: (err) => {
        console.error('Error al actualizar:', err);
        this.loading = false;
        this._snackBar.open('Error al actualizar los datos', '', {
          duration: 3000,
          horizontalPosition: 'right'
        });
      }
    });
  }

  // Si estás en modo AGREGAR
  else {
    this._veterinariaService.crearConMascota(data).subscribe({
      next: () => {
        this.loading = false;
        this._snackBar.open('Persona y mascota registradas con éxito', '', {
          duration: 3000,
          horizontalPosition: 'right'
        });
        this.routes.navigate(['/listadoGeneral']); // vuelve al listado general
      },
      error: (err) => {
        console.error('Error al crear:', err);
        this.loading = false;
        this._snackBar.open('Error al registrar los datos', '', {
          duration: 3000,
          horizontalPosition: 'right'
        });
      }
    });
  }
  }

cancelar() {
  this.routes.navigate(['/listadoGeneral']);
}


}
