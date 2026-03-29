
import { Routes } from '@angular/router';

//login
import { Login } from './components/login/login';


// Componentes de Mascota standalone
import { ListadoMascotaComponent } from './components/mascota/listado-mascota/listado-mascota.component';
import { AgregarEditarMascotaComponent } from './components/mascota/agregar-editar-mascota/agregar-editar-mascota.component'
import { VerMascotaComponent } from './components/mascota/ver-mascota/ver-mascota.component';

// componentes de Personsa
import { ListadoPersonaComponent } from './components/persona/listado-persona/listado-persona';
import { AgregarEditarPersona } from './components/persona/agregar-editar-persona/agregar-editar-persona';
import { VerPersonaComponent } from './components/persona/ver-persona/ver-persona';

// 🐾 Importar componentes
import { ListadoGeneralComponent } from './components/veterinaria/listado-general/listado-general';
import { AgregarEditarVeterinaria } from './components/veterinaria/agregar-editar-veterinaria/agregar-editar-veterinaria';
import { VerVeterinaria } from './components/veterinaria/ver-veterinaria/ver-veterinaria';


export const routes: Routes = [//ver minuto 1:00:00 explica como redireccionar las rutas en la pagina 

{ path: '' , redirectTo:'login' , pathMatch: 'full' },
{ path: 'login', component: Login},
  
  //{ path: '', redirectTo:'listadoGeneral', pathMatch: 'full'   }, // Página inicial---modificar esto ahora 

  // 🧩 Tu nuevo listado combinado
  { path: 'listadoGeneral', component: ListadoGeneralComponent },

  // Agregar nuevo registro (persona + mascota)
  { path: 'agregar-veterinaria', component: AgregarEditarVeterinaria },

  // Editar registro existente
  { path: 'editar/:id', component: AgregarEditarVeterinaria },

  // Ver detalles
  { path: 'ver/:id', component: VerVeterinaria },

// Rutas de Mascota
  { path: 'listadoMascota', component: ListadoMascotaComponent },
  { path: 'agregar', component: AgregarEditarMascotaComponent },
  { path: 'editar/:id', component: AgregarEditarMascotaComponent }, // Usamos el mismo para editar y va incrementando el id de las mascotas que muestra
  { path: 'ver/:id', component: VerMascotaComponent },

//  Rutas de Persona
  { path: 'listadoPersona' , component: ListadoPersonaComponent },
  { path: 'agregarPersona' , component: AgregarEditarPersona },
  { path: 'editarPersona/:id' , component: AgregarEditarPersona },
  { path: 'ver/:id' , component: VerPersonaComponent },


  { path: '**',  redirectTo:'listadoGeneral', pathMatch: 'full'  } // Redirige cualquier ruta desconocida al listado
];

