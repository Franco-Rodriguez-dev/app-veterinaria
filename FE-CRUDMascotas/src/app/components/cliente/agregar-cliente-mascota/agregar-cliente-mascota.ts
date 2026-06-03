import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ClienteMascotaUsuarioCreate } from '../../../interfaces/cliente';
import { ClienteService } from '../../../service/cliente';
import { MaterialModules } from '../../../shared/material';

@Component({
  selector: 'app-agregar-cliente-mascota',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MaterialModules],
  templateUrl: './agregar-cliente-mascota.html',
  styleUrls: ['./agregar-cliente-mascota.css']
})
export class AgregarClienteMascota {
  loading = false;

  private fb = inject(FormBuilder);
  private clienteService = inject(ClienteService);
  private snackBar = inject(MatSnackBar);
  private router = inject(Router);

  formCliente = this.fb.group({
    nombre: ['', Validators.required],
    apellido: ['', Validators.required],
    edad: [null as number | null, [Validators.required, Validators.min(1), Validators.max(100)]],
    sexo: ['', Validators.required],
    telefono: ['', Validators.required],
    username: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]],
    nombreMascota: ['', Validators.required],
    raza: ['', Validators.required],
    color: ['', Validators.required],
    edadMascota: [null as number | null, [Validators.required, Validators.min(0), Validators.max(25)]],
    peso: [null as number | null, [Validators.required, Validators.min(0.1), Validators.max(100)]]
  });

  guardar() {
    if (this.formCliente.invalid) {
      // Marca todos los campos como tocados para mostrar los errores del formulario.
      this.formCliente.markAllAsTouched();
      return;
    }

    // getRawValue() trae todos los valores del formulario y "as" le indica a TypeScript el tipo esperado.
    const data = this.formCliente.getRawValue() as ClienteMascotaUsuarioCreate;
    this.loading = true;

    this.clienteService.crearUsuarioConMascota(data).subscribe({
      next: (cliente) => {
        this.loading = false;
        // Muestra un mensaje usando el nombre que devuelve el backend en el DTO de respuesta.
        this.snackBar.open(
          `Cliente ${cliente.nombreCompleto} creado correctamente`,
          '',
          { duration: 3000, horizontalPosition: 'right' }
        );
        this.router.navigate(['/listadoGeneral']);
      },
      error: (err) => {
        this.loading = false;
        // Si el backend devuelve texto, lo mostramos; si devuelve otra cosa, usamos un mensaje generico.
        const mensaje = typeof err.error === 'string'
          ? err.error
          : 'Error al crear el cliente';

        this.snackBar.open(mensaje, '', {
          duration: 3500,
          horizontalPosition: 'right'
        });
      }
    });
  }

  cancelar() {
    this.router.navigate(['/listadoGeneral']);
  }
}
