import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, RouterModule } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MascotaService } from '../../../service/mascota';  // ajustá la ruta según tu proyecto
import { Mascota } from '../../../interfaces/mascota';  // o donde esté tu modelo
import { MaterialModules } from '../../../shared/material';


@Component({
  selector: 'app-ver-mascota',
  standalone: true,               // 👈 ahora es standalone
  imports: [RouterModule,MaterialModules],
  templateUrl: './ver-mascota.html',
  styleUrls: ['./ver-mascota.css'] // 👈 plural
})

export class VerMascotaComponent implements OnInit {
  private _snackBar = inject(MatSnackBar);
  private _mascotaService = inject(MascotaService);
  private aRouter = inject(ActivatedRoute);

  id: string | null = null;
 mascota : Mascota | undefined;

  ngOnInit(): void {
    // Suscribirse al paramMap para recibir cambios en el id
    this.aRouter.paramMap.subscribe((params: ParamMap) => {
      this.id = params.get('id');

      if (this.id) {
        this.cargarMascota(this.id);
      }
    });
  }

  cargarMascota(id: string): void {
    this._mascotaService.getMascotasVer(id).subscribe({
      next: (data) => {
        this.mascota = data;
      },
      error: (err) => {
        this._snackBar.open('Error al cargar mascota', 'Cerrar', {
          duration: 3000
        });
      }
    });
  }

  volver(): void {
    // Navegar de vuelta al listado
    // Por ejemplo, si usás Router:
    // this.router.navigate(['/listadoMascota']);
  }
}



