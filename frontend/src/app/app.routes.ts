import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';

export const routes: Routes = [
    {
        path: 'auth',
        loadChildren: () => import('./modules/auth/auth.routes').then(m => m.authRoutes)
    },
    {
        path: 'products',
        loadChildren: () => import('./modules/products/products.routes').then(m => m.productsRoutes),
        canActivate: [authGuard]
    },
    {
        path: 'orders',
        loadChildren: () => import('./modules/orders/orders.routes').then(m => m.ordersRoutes),
        canActivate: [authGuard]
    },
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'auth'
    }
];
