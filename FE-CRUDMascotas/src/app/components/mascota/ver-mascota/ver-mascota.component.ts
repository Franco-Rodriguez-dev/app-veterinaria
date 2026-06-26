import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router, RouterModule } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MascotaService } from '../../../service/mascota';
import { Mascota } from '../../../interfaces/mascota';
import { MaterialModules } from '../../../shared/material';
import { HistorialMascota } from '../../../interfaces/historial-mascota';
import { HistorialMascotaService } from '../../../service/historial-mascota';
import { Auth } from '../../../service/auth';
import { ClienteService } from '../../../service/cliente';

@Component({
  selector: 'app-ver-mascota',
  standalone: true,
  imports: [CommonModule, RouterModule, MaterialModules],
  templateUrl: './ver-mascota.html',
  styleUrls: ['./ver-mascota.css']
})
export class VerMascotaComponent implements OnInit {
  private _snackBar = inject(MatSnackBar);
  private _mascotaService = inject(MascotaService);
  private _clienteService = inject(ClienteService);
  private _historialService = inject(HistorialMascotaService);
  private _authService = inject(Auth);
  private aRouter = inject(ActivatedRoute);
  private router = inject(Router);

  id: string | null = null;
  mascota: Mascota | undefined;
  historial: HistorialMascota[] = [];
  rol = '';
  displayedColumns: string[] = ['fecha', 'tipo', 'titulo', 'precio', 'proximaVisita', 'acciones'];

  ngOnInit(): void {
    this.rol = this._authService.getRol() || '';

    this.aRouter.paramMap.subscribe((params: ParamMap) => {
      this.id = params.get('id');

      if (this.id) {
        this.cargarMascota(this.id);
        this.cargarHistorial(this.id);
      }
    });
  }

  cargarMascota(id: string): void {
    const mascotaRequest = this.rol === 'Cliente'
      ? this._clienteService.getMiMascota(id)
      : this._mascotaService.getMascotasVer(id);

    mascotaRequest.subscribe({
      next: (data) => {
        this.mascota = data;
      },
      error: () => {
        this._snackBar.open('Error al cargar mascota', 'Cerrar', {
          duration: 3000
        });
      }
    });
  }

  cargarHistorial(mascotaId: string | number): void {
    this._historialService.getByMascota(mascotaId).subscribe({
      next: (data) => {
        this.historial = data;
      },
      error: () => {
        this._snackBar.open('Error al cargar historial', 'Cerrar', {
          duration: 3000
        });
      }
    });
  }

  agregarHistorial(): void {
    if (!this.id) return;

    this.router.navigate(['/historial-mascota/agregar', this.id]);
  }

  editarHistorial(id: number): void {
    this.router.navigate(['/historial-mascota/editar', id]);
  }

  eliminarHistorial(id: number): void {
    if (!confirm('Seguro que queres eliminar este historial?')) return;

    this._historialService.delete(id).subscribe({
      next: () => {
        if (this.id) {
          this.cargarHistorial(this.id);
        }

        this._snackBar.open('Historial eliminado correctamente', '', {
          duration: 3000
        });
      },
      error: () => {
        this._snackBar.open('Error al eliminar historial', 'Cerrar', {
          duration: 3000
        });
      }
    });
  }

  volver(): void {
    const ruta = this.rol === 'Cliente' ? '/mi-perfil' : '/listadoGeneral';
    this.router.navigate([ruta]);
  }
}
