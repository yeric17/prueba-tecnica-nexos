import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../modules/auth/services/auth.service';

export const adminGuard: CanActivateFn = async (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const isAuth = await authService.isAuthenticated();

  if (!isAuth) {
    return router.navigate(['auth/login']);
  }

  const user = authService.user();
  
  if (user && user.roles.map(r => r.toLowerCase()).includes('admin')) {
    return true;
  }

  // Not admin - redirect to products list
  return router.navigate(['products/list']);
};
