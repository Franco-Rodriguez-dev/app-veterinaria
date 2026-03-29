import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MaterialModules } from './shared/material'; // 👈 importás todos de golpe
//import { ListadoMascotaComponent } from './components/listado-mascota/listado-mascota.component';// hay que agregar para poder usar en el html

@Component({
  selector: 'app-root',
  standalone: true,   // 👈 hay que agregar esto
  imports: [RouterOutlet,MaterialModules],//ListadoMascotaComponent,//y aca tambien se agrega y usas html
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  protected readonly title = signal('FE-CRUDMascotas');
}
