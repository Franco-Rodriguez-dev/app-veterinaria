import { Component, inject, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { VeterinariaService } from '../../../service/veterinaria';
import { ActivatedRoute, ParamMap, RouterModule } from '@angular/router';

import { MaterialModules } from '../../../shared/material';
import { VeterinariaDetalle } from '../../../interfaces/veterinaria-detalle';


@Component({
  selector: 'app-ver-veterinaria',
  standalone: true,  
  imports: [RouterModule,MaterialModules],
  templateUrl: './ver-veterinaria.html',
  styleUrls: ['./ver-veterinaria.css']
})
export class VerVeterinaria implements OnInit {
  private _snackBar = inject (MatSnackBar);
  private _veterinariaService = inject(VeterinariaService);
  private aRouter = inject(ActivatedRoute);

   id: string | null = null;
   veterinaria: VeterinariaDetalle | undefined;

   ngOnInit(): void {
    // Suscribirse al paramMap para recibir cambios en el id
    this.aRouter.paramMap.subscribe((params: ParamMap) => {
      this.id = params.get('id');

      if (this.id) {
        this.CargarVeterinaria(this.id);
      }
    });
  }

  CargarVeterinaria (id: string): void {
    this._veterinariaService.getDetalle(id).subscribe({
      next: (data) => {
        this.veterinaria = data;
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
