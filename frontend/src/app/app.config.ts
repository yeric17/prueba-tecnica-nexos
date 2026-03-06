import { ApplicationConfig, importProvidersFrom, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter, withComponentInputBinding, withViewTransitions } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { apiConnectionInterceptor } from './core/interceptors/api-connection-interceptor';
import {
  AlertTriangle,
  ArrowLeft,
  CheckCircle,
  Eraser,
  Image,
  Info,
  Lock,
  LockOpen,
  LogOut,
  LucideAngularModule,
  Minus,
  Moon,
  Pencil,
  Plus,
  ShoppingBag,
  ShoppingCart,
  Sun,
  Trash2,
  Upload,
  X,
  XCircle,
} from 'lucide-angular';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes, withComponentInputBinding(),withViewTransitions()),
    provideHttpClient(withFetch(),withInterceptors([apiConnectionInterceptor])),
    importProvidersFrom(
      LucideAngularModule.pick({
        AlertTriangle,
        ArrowLeft,
        CheckCircle,
        Eraser,
        Image,
        Info,
        Lock,
        LockOpen,
        LogOut,
        Minus,
        Moon,
        Pencil,
        Plus,
        ShoppingBag,
        ShoppingCart,
        Sun,
        Trash2,
        Upload,
        X,
        XCircle,
      })
    )
  ]
};
