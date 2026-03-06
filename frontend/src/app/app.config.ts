import { ApplicationConfig, importProvidersFrom, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter, withComponentInputBinding, withViewTransitions } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { apiConnectionInterceptor } from './core/interceptors/api-connection-interceptor';
import {
  ArrowLeft,
  Image,
  Lock,
  LockOpen,
  LogOut,
  LucideAngularModule,
  Moon,
  Pencil,
  Plus,
  ShoppingCart,
  Sun,
  Trash2,
  Upload,
  X,
} from 'lucide-angular';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes, withComponentInputBinding(),withViewTransitions()),
    provideHttpClient(withFetch(),withInterceptors([apiConnectionInterceptor])),
    importProvidersFrom(
      LucideAngularModule.pick({
        ArrowLeft,
        Image,
        Lock,
        LockOpen,
        LogOut,
        Moon,
        Pencil,
        Plus,
        ShoppingCart,
        Sun,
        Trash2,
        Upload,
        X,
      })
    )
  ]
};
