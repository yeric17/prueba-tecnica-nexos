import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../../modules/auth/services/auth.service';
import { environment } from '../../../environments/environment';

export const apiConnectionInterceptor: HttpInterceptorFn = (req, next) => {

  const authService = inject(AuthService)

  const token = authService.getToken()

  if(req.url.includes('users-service') && token){
    const newReq = req.clone({
      setHeaders: {
        'Authorization': `Bearer ${token.accessToken}`
      }
    })
    return next(newReq)
  }

  return next(req);
};
