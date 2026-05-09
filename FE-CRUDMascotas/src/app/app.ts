import { Component, signal, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MaterialModules } from './shared/material';
import { Navbar } from './shared/navbar/navbar';
import { CommonModule } from '@angular/common';
import { Auth } from './service/auth';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MaterialModules, Navbar, CommonModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {

  protected readonly title = signal('FE-CRUDMascotas');

  authService = inject(Auth); // 🔥 usamos el servicio reactivo
}