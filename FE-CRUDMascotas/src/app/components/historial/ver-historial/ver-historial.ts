import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MaterialModules } from '../../../shared/material';
import { HistorialMascota } from '../../../interfaces/historial-mascota';
import { HistorialMascotaService } from '../../../service/historial-mascota';

@Component({
  selector: 'app-ver-historial',
  standalone: true,
  imports: [CommonModule, RouterModule, MaterialModules],
  templateUrl: './ver-historial.html',
  styleUrls: ['./ver-historial.css']
})
export class VerHistorial {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);
  private historialService = inject(HistorialMascotaService);

  historial: HistorialMascota | undefined;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (!id) return;

    this.historialService.getById(id).subscribe({
      next: (data) => {
        this.historial = data;
      },
      error: () => {
        this.snackBar.open('Error al cargar historial', 'Cerrar', { duration: 3000 });
      }
    });
  }

  volver(): void {
    if (this.historial?.mascotaId) {
      this.router.navigate(['/mascota/ver', this.historial.mascotaId]);
      return;
    }

    this.router.navigate(['/listadoMascota']);
  }
}
