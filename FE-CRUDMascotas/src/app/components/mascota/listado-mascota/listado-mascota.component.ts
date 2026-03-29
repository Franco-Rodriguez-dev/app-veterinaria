import { AfterViewInit, Component, inject, OnInit, ViewChild } from '@angular/core';
import { MaterialModules } from '../../../shared/material'; //agregar para importar materialmodules
import { MascotaService } from '../../../service/mascota';
import { Mascota } from '../../../interfaces/mascota';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
//lo tenia que agregar para usar *ngif
import { NgIf } from '@angular/common';
import { Spinner } from '../../../shared/spinner/spinner';



@Component({
  selector: 'app-listado-mascota',
  standalone: true,
  imports: [NgIf,RouterModule,MaterialModules,Spinner],
  templateUrl: './listado-mascota.html',
  styleUrls: ['./listado-mascota.css']
})


export class ListadoMascotaComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['nombre','edad','raza','color','peso'];//,'acciones'
  dataSource = new MatTableDataSource<Mascota>();
  loading: boolean = false;



  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

   // 🔹 Aquí cargamos los datos al iniciar el componente
  ngOnInit(): void {
    this.obtenerMascota();
  }

  //se lo agregue recien 
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.paginator._intl.itemsPerPageLabel ='items por pagina'//aca se cambia los nombres de paginator
    this.dataSource.sort = this.sort;

  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

   //A partir de Angular 14+, tenés la API inject(), que permite inyectar dependencias sin necesidad de un constructor:
  private _snackBar = inject(MatSnackBar)
  private _mascotaService = inject(MascotaService)//de service/mascota.ts ponemos la class en forma inyectada
  private aRouter = inject(ActivatedRoute)//en este caso para injectar el id 

   id = this.aRouter.snapshot.paramMap.get('id');

  obtenerMascota(){
    this.loading = true;
    this._mascotaService.getMascota().subscribe(data =>{
    this.loading = false;
      this.dataSource.data = data;
    })
  }

  eliminarMascota( id : number){
    this.loading= true;

  this._mascotaService.deleteMascota(id).subscribe (() => {
    this.MensajeExito();
    this.loading = false;
    this.obtenerMascota();
  })
  
  }
  
  MensajeExito(){
     this._snackBar.open('la mascota fue eliminada/o con exito', '',{
         duration:4000,
         horizontalPosition: 'right',
      });

  }





}