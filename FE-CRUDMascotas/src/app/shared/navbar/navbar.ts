import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { Auth } from '../../service/auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule],
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

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}