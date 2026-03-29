import { Component} from '@angular/core';
import { MaterialModules } from '../../shared/material';
import { Router} from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Auth } from '../../service/auth';

import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoginRequest } from '../../interfaces/login-request';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MaterialModules,FormsModule,CommonModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class Login {
   usuario: string = '';
  password: string = '';

  constructor(
    private authService :Auth,
    private http :HttpClient,
  
    private router: Router 
  ) {}

  login() {

    const body : LoginRequest  = {
      username: this.usuario,
      password: this.password
    };

    this.authService.login(body).subscribe(res => {

    this.authService.saveSession(res);

    this.router.navigate(['/listadoGeneral']);
      });
  }
}


