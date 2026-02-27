import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../modules/auth/services/auth.service';

export const authGuard: CanActivateFn = async (route, state) => {
  const authService = inject(AuthService)
  const router = inject(Router)

  const isAuth = await authService.isAuthenticated()

  if(isAuth){
    return true
  }

  return router.navigate(['auth/login']);
};
