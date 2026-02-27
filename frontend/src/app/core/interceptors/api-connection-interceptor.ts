import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../../modules/auth/services/auth.service';

export const apiConnectionInterceptor: HttpInterceptorFn = (req, next) => {

  const authService = inject(AuthService)

  const token = authService.getToken()

  if(req.url.includes('/api/') && token){
    const newReq = req.clone({
      setHeaders: {
        'Authorization': `Bearer ${token.accessToken}`
      }
    })
    return next(newReq)
  }

  return next(req);
};
