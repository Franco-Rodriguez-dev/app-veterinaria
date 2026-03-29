import { AfterViewInit, Component, inject, OnInit, ViewChild } from '@angular/core';
import { MaterialModules } from '../../../shared/material';
import { PersonaService } from '../../../service/persona';
import { persona } from '../../../interfaces/persona';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NgIf } from '@angular/common';
import { Spinner } from '../../../shared/spinner/spinner';

@Component({
  selector: 'app-listado-persona',
  standalone: true,
  imports: [NgIf, RouterModule, MaterialModules, Spinner],
  templateUrl: './listado-persona.html',
  styleUrls: ['./listado-persona.css']
})
export class ListadoPersonaComponent implements OnInit, AfterViewInit {
  // 🔹 Columnas que se mostrarán en la tabla
  displayedColumns: string[] = ['nombre', 'apellido', 'edad', 'telefono', 'cantMascotas']; //, 'acciones'

  // 🔹 MatTableDataSource es la fuente de datos de la tabla
  dataSource = new MatTableDataSource<persona>();

  // 🔹 Controla si se muestra o no el spinner (true = cargando)
  loading: boolean = false;

  // 🔹 Referencias a los componentes de paginación y ordenamiento en el HTML
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  // 🔹 Inyección moderna (sin constructor)
  private _snackBar = inject(MatSnackBar); // Para mostrar mensajes flotantes
  private _personaService = inject(PersonaService); // Servicio que se comunica con la API
  private aRouter = inject(ActivatedRoute); // Permite obtener parámetros de la URL (por ejemplo, el ID)

  // ===========================================================
  // 🟢 ngOnInit(): se ejecuta al iniciar el componente
  // ===========================================================
  ngOnInit(): void {
    // Apenas se abre el componente, se cargan todas las personas desde la API
    this.obtenerPersonas();
  }

  // ===========================================================
  // 🟢 ngAfterViewInit(): se ejecuta cuando el HTML ya está cargado
  // ===========================================================
  ngAfterViewInit() {
    // Asocia el paginador y el ordenamiento a la tabla
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    // Traduce el texto del paginador
    this.paginator._intl.itemsPerPageLabel = 'items por página';
  }

  // ===========================================================
  // 🔹 applyFilter(): filtra la tabla según lo que el usuario escribe
  // ===========================================================
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value; // Obtiene el texto del input
    this.dataSource.filter = filterValue.trim().toLowerCase(); // Filtra ignorando mayúsculas/minúsculas
  }

  // ===========================================================
  // 🟢 obtenerPersonas(): obtiene la lista completa desde el backend
  // ===========================================================
  obtenerPersonas() {
    this.loading = true; // Muestra el spinner
    this._personaService.getPersonas().subscribe(data => { // Llama al endpoint GET api/persona
      this.loading = false; // Oculta el spinner
      this.dataSource.data = data; // Asigna los datos recibidos a la tabla
    });
  }

  // ===========================================================
  // 🟠 eliminarPersona(): elimina una persona por ID
  // ===========================================================
  eliminarPersona(id: number) {
    this.loading = true; // Muestra spinner
    this._personaService.deletePersona(id).subscribe(() => { // Llama al DELETE api/persona/{id}
      this.MensajeExito(); // Muestra mensaje de éxito
      this.loading = false; // Oculta spinner
      this.obtenerPersonas(); // Recarga la tabla para actualizar los datos
    });
  }

  // ===========================================================
  // 🔹 MensajeExito(): muestra un aviso con MatSnackBar
  // ===========================================================
  MensajeExito() {
    this._snackBar.open('La persona fue eliminada con éxito', '', {
      duration: 4000, // Cuánto tiempo se muestra el mensaje (ms)
      horizontalPosition: 'right', // Posición en pantalla
    });
  }
}