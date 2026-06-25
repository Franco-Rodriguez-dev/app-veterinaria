import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Auth } from '../service/auth';

export const passwordChangeGuard: CanActivateFn = (route, state) => {
  const auth = inject(Auth);
  const router = inject(Router);

  if (auth.isLogged() && auth.debeCambiarPassword()) {
    return router.createUrlTree(['/cambiar-password']);
  }

  return true;
};
