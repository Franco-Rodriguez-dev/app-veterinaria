import { Component, OnInit } from '@angular/core';
import { MaterialModules } from './../../../shared/material';
import { Spinner } from './../../../shared/spinner/spinner';
import { NgIf } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';//agregar si o si para poder usar routerlink
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Mascota } from './../../../interfaces/mascota';
import { MascotaService } from './../../../service/mascota';
import { MatSnackBar } from '@angular/material/snack-bar';
 

@Component({
  selector: 'app-agregar-editar-mascota',
  standalone: true,
  imports: [NgIf,MaterialModules,Spinner,RouterModule],
  templateUrl: './agregar-editar-mascota.html',
  styleUrls: ['./agregar-editar-mascota.css']
})
export class AgregarEditarMascotaComponent implements OnInit {
loading: boolean =false;

//todo esto esta en el minuto 142 . todo lo que es formulario , de la pagina 
mascotaForm: FormGroup;
id: number;
operacion : string = 'Agregar';

constructor(private fb: FormBuilder , private _mascotaService: MascotaService,
   private _snackBar : MatSnackBar , private routes : Router , private aRoute : ActivatedRoute ){

  this.mascotaForm = this.fb.group({
    nombre: ['',Validators.required],
    raza: ['',Validators.required],
    color: ['',Validators.required],
    edad: ['',Validators.required],//edad: [0, [Validators.required, Validators.min(1)]]
    peso: ['',Validators.required],//el validators.required es para que si o si complete el campo si no , no se guarda el form

  })

  this.id = Number(this.aRoute.snapshot.paramMap.get('id'));

}

//lo que hacemos es cambiar lo que le asignamos a la variable operacion, cuando es distinto de cero se pone Editar.
//ciclo de vida
 ngOnInit(): void {
    if(this.id != 0 ){
      this.operacion = 'Editar';
      this.obtenerMascota(this.id);
    }
  }

obtenerMascota(id : number){
  this.loading= true;
  this._mascotaService.getMascotasVer(id).subscribe(data => {
    this.mascotaForm.setValue({//patchValue o setValue . el patch se usa para agregar algunos campos y el otro tiene que ser completo
      nombre : data.nombre,
      raza : data.raza,
      color : data.color,
      edad : data.edad,
      peso : data.peso
    })
    this.loading = false;
  })
}

agregarEditarMascota(){
  //puede ser de cualquiera de las dos formas sirve para recuperar lo que el usuario llenó en el form y poder guardarlo, procesarlo o enviarlo.
  //const nombre=this.mascotaForm.get('nombre')?.value  ---este es mas para un solo objeto
 // const nombre= this.mascotaForm.value.nombre; //este es mejor si queres agarrar el formulario completo

  const mascota: Mascota = {
    nombre:this.mascotaForm.value.nombre,
    raza: this.mascotaForm.value.raza,
    color: this.mascotaForm.value.color,
    edad: this.mascotaForm.value.edad,
    peso: this.mascotaForm.value.peso
  }

  if (this.id != 0) {
    mascota.id = this.id;
    this.EditarMascota(this.id,mascota);
  } else {
    this.AgregarMascota(mascota);
  }

}

EditarMascota(id : number , mascota : Mascota){
  this.loading = true;
  this._mascotaService.updateMascota(id,mascota).subscribe(() => {
    this.loading = false;
    this.MensajeExito('actualizada');
    this.routes.navigate(['/listadoMascota']);

  })
}


AgregarMascota(mascota : Mascota){
   //enviamos el objeto al back-end

  this._mascotaService.addMascota(mascota).subscribe(data => {
    this.MensajeExito('registrada');
    this.routes.navigate(['/listadoMascota']);
  })

}

 MensajeExito( texto : string){
     this._snackBar.open(`la mascota fue ${texto} con exito`, '',{
         duration:4000,
         horizontalPosition: 'right',
      });

  }



}
