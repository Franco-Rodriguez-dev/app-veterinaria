import { CommonModule } from '@angular/common';
import { Component, inject, ViewChild } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ClienteInactivo } from '../../../interfaces/veterinaria';
import { VeterinariaService } from '../../../service/veterinaria';
import { MaterialModules } from '../../../shared/material';
import { Spinner } from '../../../shared/spinner/spinner';

@Component({
  selector: 'app-clientes-inactivos',
  standalone: true,
  imports: [CommonModule, RouterModule, MaterialModules, Spinner],
  templateUrl: './clientes-inactivos.html',
  styleUrls: ['./clientes-inactivos.css']
})
export class ClientesInactivos {
  displayedColumns: string[] = ['nombre', 'apellido', 'telefono', 'username', 'cantidadMascotas', 'acciones'];
  dataSource = new MatTableDataSource<ClienteInactivo>();
  loading = false;

  private veterinariaService = inject(VeterinariaService);
  private snackBar = inject(MatSnackBar);
  private router = inject(Router);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void {
    this.obtenerClientesInactivos();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  obtenerClientesInactivos(): void {
    this.loading = true;

    this.veterinariaService.getClientesInactivos().subscribe({
      next: (data) => {
        this.dataSource.data = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.snackBar.open('Error al cargar clientes dados de baja', 'Cerrar', { duration: 3000 });
      }
    });
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  reactivar(cliente: ClienteInactivo): void {
    if (!confirm(`Seguro que queres reactivar a ${cliente.nombre} ${cliente.apellido}?`)) return;

    this.veterinariaService.reactivarCliente(cliente.personaId).subscribe({
      next: (mensaje) => {
        this.snackBar.open(mensaje, 'Cerrar', { duration: 3000 });
        this.obtenerClientesInactivos();
      },
      error: (err) => {
        const mensaje = typeof err.error === 'string'
          ? err.error
          : 'No se pudo reactivar el cliente';

        this.snackBar.open(mensaje, 'Cerrar', { duration: 3000 });
      }
    });
  }

  volver(): void {
    this.router.navigate(['/listadoGeneral']);
  }
}
