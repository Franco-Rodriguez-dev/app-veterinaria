import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MaterialModules } from '../../../shared/material';
import { MiPerfilCliente } from '../../../interfaces/cliente';
import { ClienteService } from '../../../service/cliente';

@Component({
  selector: 'app-mi-perfil-cliente',
  standalone: true,
  imports: [CommonModule, MaterialModules],
  templateUrl: './mi-perfil-cliente.html',
  styleUrls: ['./mi-perfil-cliente.css']
})
export class MiPerfilClienteComponent {
  private clienteService = inject(ClienteService);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);

  perfil: MiPerfilCliente | undefined;
  loading = true;

  ngOnInit(): void {
    this.clienteService.getMiPerfil().subscribe({
      next: (data) => {
        this.perfil = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.snackBar.open('No se pudo cargar tu perfil', 'Cerrar', { duration: 3000 });
      }
    });
  }

  verHistorial(mascotaId: number): void {
    this.router.navigate(['/mascota/ver', mascotaId]);
  }
}
