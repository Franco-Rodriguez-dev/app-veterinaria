
import { Routes } from '@angular/router';

//login
import { Login } from './components/login/login';

//auth
import { authGuard } from './guards/auth-guard';
import { roleGuard } from './guards/role-guard';


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
import { ClientesInactivos } from './components/veterinaria/clientes-inactivos/clientes-inactivos';

// componentes de Historial
import { AgregarEditarHistorial } from './components/historial/agregar-editar-historial/agregar-editar-historial';
import { VerHistorial } from './components/historial/ver-historial/ver-historial';

// componentes de Cliente
import { AgregarClienteMascota } from './components/cliente/agregar-cliente-mascota/agregar-cliente-mascota';
import { MiPerfilClienteComponent } from './components/cliente/mi-perfil-cliente/mi-perfil-cliente';


export const routes: Routes = [//ver minuto 1:00:00 explica como redireccionar las rutas en la pagina 

{ path: '' , redirectTo:'login' , pathMatch: 'full' },
{ path: 'login', component: Login},
  
  //{ path: '', redirectTo:'listadoGeneral', pathMatch: 'full'   }, // Página inicial---modificar esto ahora 

  // 🧩 Tu nuevo listado combinado
  { path: 'listadoGeneral', component: ListadoGeneralComponent, canActivate: [authGuard, roleGuard], data: { roles: ['Administrador'] } },
  { path: 'clientes-inactivos', component: ClientesInactivos, canActivate: [authGuard, roleGuard], data: { roles: ['Administrador'] } },

  // Agregar nuevo registro (persona + mascota)
  { path: 'agregar-veterinaria', component: AgregarEditarVeterinaria , canActivate: [authGuard, roleGuard],data: { roles: ['Administrador'] }},

  // Crear cliente con usuario y mascota inicial
  { path: 'cliente/agregar-con-mascota', component: AgregarClienteMascota, canActivate: [authGuard, roleGuard], data: { roles: ['Administrador'] }},

  // Perfil del cliente logueado
  { path: 'mi-perfil', component: MiPerfilClienteComponent, canActivate: [authGuard, roleGuard], data: { roles: ['Cliente'] }},

  // Editar registro existente
  { path: 'editar/:id', component: AgregarEditarVeterinaria, canActivate: [authGuard, roleGuard], data: { roles: ['Administrador'] } },

  // Ver detalles
  { path: 'ver/:id', component: VerVeterinaria, canActivate: [authGuard, roleGuard], data: { roles: ['Administrador'] } },

// Rutas de Mascota
  { path: 'listadoMascota', component: ListadoMascotaComponent, canActivate: [authGuard, roleGuard], data: { roles: ['Administrador'] } },
  { path: 'mascota/agregar', component: AgregarEditarMascotaComponent, canActivate:[authGuard, roleGuard], data: { roles: ['Administrador'] }},
  { path: 'mascota/editar/:id', component: AgregarEditarMascotaComponent, canActivate: [authGuard, roleGuard], data: { roles: ['Administrador'] } }, // Usamos el mismo para editar y va incrementando el id de las mascotas que muestra
  { path: 'mascota/ver/:id', component: VerMascotaComponent, canActivate: [authGuard] },

// Rutas de Historial de Mascota
  { path: 'historial-mascota/agregar/:mascotaId', component: AgregarEditarHistorial, canActivate:[authGuard, roleGuard], data: { roles: ['Administrador'] } },
  { path: 'historial-mascota/editar/:id', component: AgregarEditarHistorial, canActivate:[authGuard, roleGuard], data: { roles: ['Administrador'] } },
  { path: 'historial-mascota/ver/:id', component: VerHistorial, canActivate: [authGuard] },

//  Rutas de Persona
  { path: 'listadoPersona' , component: ListadoPersonaComponent, canActivate: [authGuard, roleGuard], data: { roles: ['Administrador'] } },
  { path: 'agregarPersona' , component: AgregarEditarPersona, canActivate:[authGuard, roleGuard], data: { roles: ['Administrador'] } },
  { path: 'editarPersona/:id' , component: AgregarEditarPersona, canActivate: [authGuard, roleGuard], data: { roles: ['Administrador'] } },
  { path: 'persona/ver/:id' , component: VerPersonaComponent, canActivate: [authGuard, roleGuard], data: { roles: ['Administrador'] } },


  { path: '**',  redirectTo:'listadoGeneral', pathMatch: 'full'  } // Redirige cualquier ruta desconocida al listado
];

