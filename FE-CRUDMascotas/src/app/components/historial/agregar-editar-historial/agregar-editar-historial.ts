import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MaterialModules } from '../../../shared/material';
import { HistorialMascotaService } from '../../../service/historial-mascota';
import {
  HistorialMascotaCreate,
  HistorialMascotaUpdate,
  TipoHistorialMascota
} from '../../../interfaces/historial-mascota';

@Component({
  selector: 'app-agregar-editar-historial',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, MaterialModules],
  templateUrl: './agregar-editar-historial.html',
  styleUrls: ['./agregar-editar-historial.css']
})
export class AgregarEditarHistorial implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);
  private historialService = inject(HistorialMascotaService);

  formHistorial: FormGroup;
  tipos: TipoHistorialMascota[] = [];
  mascotaId: number | null = null;
  historialId: number | null = null;
  operacion = 'Agregar';
  loading = false;

  constructor() {
    this.formHistorial = this.fb.group({
      fecha: [this.toDateInputValue(new Date()), Validators.required],
      tipo: ['', Validators.required],
      titulo: ['', [Validators.required, Validators.maxLength(80)]],
      descripcion: ['', [Validators.required, Validators.maxLength(500)]],
      observaciones: ['', Validators.maxLength(500)],
      precio: [null, Validators.min(0)],
      proximaVisita: [null]
    });
  }

  ngOnInit(): void {
    const agregarMascotaId = this.route.snapshot.paramMap.get('mascotaId');
    const editarId = this.route.snapshot.paramMap.get('id');

    this.mascotaId = agregarMascotaId ? Number(agregarMascotaId) : null;
    this.historialId = editarId ? Number(editarId) : null;

    if (this.historialId) {
      this.operacion = 'Editar';
      this.cargarHistorial(this.historialId);
    }

    this.cargarTipos();
  }

  cargarTipos(): void {
    this.historialService.getTipos().subscribe({
      next: (tipos) => {
        this.tipos = tipos;
      },
      error: () => {
        this.snackBar.open('Error al cargar tipos de historial', 'Cerrar', {
          duration: 3000
        });
      }
    });
  }

  cargarHistorial(id: number): void {
    this.loading = true;

    this.historialService.getById(id).subscribe({
      next: (historial) => {
        this.mascotaId = historial.mascotaId;
        this.formHistorial.patchValue({
          fecha: this.toDateInputValue(historial.fecha),
          tipo: historial.tipo,
          titulo: historial.titulo,
          descripcion: historial.descripcion,
          observaciones: historial.observaciones || '',
          precio: historial.precio ?? null,
          proximaVisita: historial.proximaVisita
            ? this.toDateInputValue(historial.proximaVisita)
            : null
        });
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.snackBar.open('Error al cargar historial', 'Cerrar', {
          duration: 3000
        });
      }
    });
  }

  guardar(): void {
    if (this.formHistorial.invalid) {
      this.formHistorial.markAllAsTouched();
      return;
    }

    if (this.historialId) {
      this.actualizar();
      return;
    }

    this.crear();
  }

  crear(): void {
    if (!this.mascotaId) return;

    const data: HistorialMascotaCreate = {
      mascotaId: this.mascotaId,
      ...this.normalizeFormValue()
    };

    this.loading = true;

    this.historialService.create(data).subscribe({
      next: () => {
        this.loading = false;
        this.snackBar.open('Historial creado correctamente', '', { duration: 3000 });
        this.volver();
      },
      error: () => {
        this.loading = false;
        this.snackBar.open('Error al crear historial', 'Cerrar', { duration: 3000 });
      }
    });
  }

  actualizar(): void {
    if (!this.historialId) return;

    const data: HistorialMascotaUpdate = this.normalizeFormValue();
    this.loading = true;

    this.historialService.update(this.historialId, data).subscribe({
      next: () => {
        this.loading = false;
        this.snackBar.open('Historial actualizado correctamente', '', { duration: 3000 });
        this.volver();
      },
      error: () => {
        this.loading = false;
        this.snackBar.open('Error al actualizar historial', 'Cerrar', { duration: 3000 });
      }
    });
  }

  volver(): void {
    if (this.mascotaId) {
      this.router.navigate(['/mascota/ver', this.mascotaId]);
      return;
    }

    this.router.navigate(['/listadoMascota']);
  }

  private normalizeFormValue(): HistorialMascotaUpdate {
    const value = this.formHistorial.value;

    return {
      fecha: value.fecha,
      tipo: value.tipo,
      titulo: value.titulo,
      descripcion: value.descripcion,
      observaciones: value.observaciones || '',
      precio: value.precio === '' || value.precio === null ? null : Number(value.precio),
      proximaVisita: value.proximaVisita || null
    };
  }

  private toDateInputValue(date: Date | string): string {
    const parsedDate = typeof date === 'string' ? new Date(date) : date;
    return parsedDate.toISOString().slice(0, 10);
  }
}
