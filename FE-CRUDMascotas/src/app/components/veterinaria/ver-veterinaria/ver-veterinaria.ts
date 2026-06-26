import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, ParamMap, Router, RouterModule } from '@angular/router';
import { VeterinariaDetalle } from '../../../interfaces/veterinaria-detalle';
import { VeterinariaService } from '../../../service/veterinaria';
import { MaterialModules } from '../../../shared/material';

@Component({
  selector: 'app-ver-veterinaria',
  standalone: true,
  imports: [CommonModule, RouterModule, MaterialModules],
  templateUrl: './ver-veterinaria.html',
  styleUrls: ['./ver-veterinaria.css']
})
export class VerVeterinaria implements OnInit {
  private _snackBar = inject(MatSnackBar);
  private _veterinariaService = inject(VeterinariaService);
  private aRouter = inject(ActivatedRoute);
  private router = inject(Router);

  id: string | null = null;
  mascotaId: number | null = null;
  veterinaria: VeterinariaDetalle | undefined;

  ngOnInit(): void {
    this.aRouter.queryParamMap.subscribe((params) => {
      const mascotaId = params.get('mascotaId');
      this.mascotaId = mascotaId ? Number(mascotaId) : null;
    });

    this.aRouter.paramMap.subscribe((params: ParamMap) => {
      this.id = params.get('id');

      if (this.id) {
        this.CargarVeterinaria(this.id);
      }
    });
  }

  CargarVeterinaria(id: string): void {
    this._veterinariaService.getDetalle(id).subscribe({
      next: (data) => {
        this.veterinaria = data;
      },
      error: () => {
        this._snackBar.open('Error al cargar mascota', 'Cerrar', {
          duration: 3000
        });
      }
    });
  }

  verHistorialClinico(): void {
    if (!this.mascotaId) {
      this._snackBar.open('No se pudo identificar la mascota', 'Cerrar', { duration: 3000 });
      return;
    }

    this.router.navigate(['/mascota/ver', this.mascotaId]);
  }

  agregarHistorialClinico(): void {
    if (!this.mascotaId) {
      this._snackBar.open('No se pudo identificar la mascota', 'Cerrar', { duration: 3000 });
      return;
    }

    this.router.navigate(['/historial-mascota/agregar', this.mascotaId]);
  }

  volver(): void {
    this.router.navigate(['/listadoGeneral']);
  }
}
