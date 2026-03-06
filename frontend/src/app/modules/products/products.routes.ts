import { Route } from "@angular/router";
import { adminGuard } from "../../core/guards/admin-guard";

export const productsRoutes :Route[] = [
    {
        path:'',
        loadComponent: () => import('../../shared/layouts/base-layout/base-layout').then(m => m.BaseLayout),
        children: [
            {
                path: 'list',
                loadComponent: () => import('./pages/products-list-page/products-list-page').then(m => m.ProductsListPage)
            },
            {
                path: 'manager',
                loadComponent: () => import('./pages/products-manager-page/products-manager-page').then(m => m.ProductsManagerPage),
                canActivate: [adminGuard]
            }
        ]
    },
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'list'
    }
]