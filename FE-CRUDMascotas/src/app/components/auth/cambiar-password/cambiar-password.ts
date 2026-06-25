import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MaterialModules } from '../../../shared/material';
import { CambiarPasswordRequest } from '../../../interfaces/cambiar-password';
import { Auth } from '../../../service/auth';

@Component({
  selector: 'app-cambiar-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MaterialModules],
  templateUrl: './cambiar-password.html',
  styleUrls: ['./cambiar-password.css']
})
export class CambiarPassword {
  private fb = inject(FormBuilder);
  private _snackBar = inject(MatSnackBar);
  private router = inject(Router);
  private authService = inject(Auth);

  formCambiarPassword = this.fb.group({
    passwordActual: ['', Validators.required],
    passwordNueva: ['', [Validators.required, Validators.minLength(6)]],
    confirmarPasswordNueva: ['', [Validators.required, Validators.minLength(6)]]
  });

  cambiarPassword() {
    if (this.formCambiarPassword.invalid) {
      return;
    }

    const form = this.formCambiarPassword.getRawValue();

    if (form.passwordNueva !== form.confirmarPasswordNueva) {
      this._snackBar.open('Las contrasenas no coinciden', 'Cerrar', { duration: 3000 });
      return;
    }

    const body: CambiarPasswordRequest = {
      passwordActual: form.passwordActual ?? '',
      passwordNueva: form.passwordNueva ?? ''
    };

    this.authService.cambiarPassword(body).subscribe({
      next: (res) => {
        this._snackBar.open(res, 'Cerrar', { duration: 3000 });
        localStorage.setItem('debeCambiarPassword', 'false');
        this.router.navigate(['/mi-perfil']);
      },
      error: (err) => {
        const mensaje = err.error || 'Error al cambiar la contrasena';
        this._snackBar.open(mensaje, 'Cerrar', { duration: 3000 });
      }
    });
  }
}
