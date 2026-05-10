import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Auth } from '../service/auth';

export const roleGuard: CanActivateFn = (route, state) => {

  const auth = inject(Auth);
  const router = inject(Router);

  const rol = auth.getRol();

  // roles permitidos definidos en la ruta
 const rolesPermitidos = route.data?.['roles'] || [];

  if (rol && rolesPermitidos.includes(rol)) {
    return true;
  }

  // ❌ no autorizado
  router.navigate(['/login']);
  return false;
};