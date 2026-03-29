import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { VeterinariaService } from '../../../service/veterinaria';
import { Veterinaria } from '../../../interfaces/veterinaria';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, RouterModule } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
//import { NgIf } from '@angular/common';
import { MaterialModules } from '../../../shared/material';
import { Spinner } from '../../../shared/spinner/spinner';
import { CommonModule } from '@angular/common';
//import { Spinner } from '../../../shared/spinner/spinner';


@Component({
  selector: 'app-listado-general',
  standalone: true,
  imports: [ CommonModule, MaterialModules, Spinner,RouterModule],
  templateUrl: './listado-general.html',
  styleUrls: ['./listado-general.css']
})
export class ListadoGeneralComponent implements OnInit {
  displayedColumns: string[] = ['nombre', 'apellido', 'telefono', 'nombreMascota', 'raza', 'peso', 'acciones'];
  dataSource = new MatTableDataSource<Veterinaria>();
  loading = false;
  rol: string = '';

  private _veterinariaService = inject(VeterinariaService);
  private _snackBar = inject(MatSnackBar);
  private router = inject(Router);
 


  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void {
    this.rol = localStorage.getItem('rol') || '';
    this.obtenerDatos();
  }

  //se lo agregue recien, se le agrega esto para la paginacion
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.paginator._intl.itemsPerPageLabel ='items por pagina'//aca se cambia los nombres de paginator
    this.dataSource.sort = this.sort;

  }

  obtenerDatos() {
    this.loading = true;
    this._veterinariaService.getListadoGeneral().subscribe({
      next: (data) => {
        this.dataSource.data = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this._snackBar.open('Error al cargar los datos', '', { duration: 3000 });
      }
    });
  }

  // 👇👇 Las funciones que querías completar
  ver(e: Veterinaria) {
    this.router.navigate(['/ver', e.personaId]);
  }

  editar(e: Veterinaria) {
    this.router.navigate(['/editar', e.personaId]);
  }

  eliminar(e: Veterinaria) {
    if (confirm(`¿Seguro que querés eliminar a ${e.nombre} y su mascota ${e.nombreMascota}?`)) {
      // Acá podrías tener un endpoint que borre ambos registros juntos
      this._veterinariaService.deleteConMascotas(e.personaId).subscribe({
        next: () => {
          this._snackBar.open('Registro eliminado con éxito', '', { duration: 3000 });
          this.obtenerDatos(); // refresca tabla
        },
        error: () => {
          this._snackBar.open('Error al eliminar el registro', '', { duration: 3000 });
        }
      });
    }
  }

  // 🟢 Agregar función para filtrar
applyFilter(event: Event) {
  const filterValue = (event.target as HTMLInputElement).value;
  this.dataSource.filter = filterValue.trim().toLowerCase();
}

// 🟢 Agregar función para botón Agregar
agregar() {
  this.router.navigate(['/agregar-veterinaria']);} 



}
