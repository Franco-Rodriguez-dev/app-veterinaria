import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Auth } from '../../service/auth';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule, MatButtonModule, MatIconModule],
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.css'],
})
export class Navbar {

  private authService = inject(Auth);
  private router = inject(Router);

  isLoggedIn$ = this.authService.isLoggedIn$;

  get isLoginPage(): boolean {
    return this.router.url === '/login';
  }

  get rol(): string {
    return this.authService.getRol() || '';
  }

  get isAdmin(): boolean {
    return this.rol === 'Administrador';
  }

  get isCliente(): boolean {
    return this.rol === 'Cliente';
  }

  get debeCambiarPassword(): boolean {
    return this.authService.debeCambiarPassword();
  }

  get passwordLinkText(): string {
    return this.debeCambiarPassword ? 'Cambiar mi contraseña' : 'Mi contraseña';
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
