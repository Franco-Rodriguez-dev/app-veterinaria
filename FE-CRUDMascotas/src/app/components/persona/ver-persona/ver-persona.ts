import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, RouterModule } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PersonaService } from '../../../service/persona';
import { PersonaDetalle } from '../../../interfaces/persona';
import { MaterialModules } from '../../../shared/material';
import { NgIf, NgFor } from '@angular/common'; //  agrega esto

@Component({
  selector: 'app-ver-persona',
  standalone: true,
  imports: [RouterModule, MaterialModules, NgIf, NgFor],
  templateUrl: './ver-persona.html',
  styleUrls: ['./ver-persona.css']
})
export class VerPersonaComponent implements OnInit {
  // Inyecciones modernas (sin constructor)
  private _snackBar = inject(MatSnackBar);
  private _personaService = inject(PersonaService);
  private aRouter = inject(ActivatedRoute);

  // Propiedades
  id: string | null = null; // ID recibido desde la URL
  persona: PersonaDetalle | undefined; // Persona a mostrar

  // ===========================================================
  // ngOnInit(): se ejecuta al iniciar el componente
  // ===========================================================
  ngOnInit(): void {
    // Nos suscribimos a los cambios del parámetro "id"
    this.aRouter.paramMap.subscribe((params: ParamMap) => {
      this.id = params.get('id');

      // Si existe un id en la URL, buscamos la persona
      if (this.id) {
        this.cargarPersona(this.id);
      }
    });
  }

  // ===========================================================
  //  cargarPersona(): llama al backend para obtener la persona
  // ===========================================================
  cargarPersona(id: string): void {
    this._personaService.getPersonaById(Number(id)).subscribe({
      next: (data) => {
        this.persona = data;
      },
      error: (err) => {
        this._snackBar.open('Error al cargar la persona', 'Cerrar', {
          duration: 3000
        });
      }
    });
  }

  // ===========================================================
  // 🔹 volver(): botón para regresar al listado
  // ===========================================================
  volver(): void {
    history.back(); // ← vuelve a la página anterior
  }
}
