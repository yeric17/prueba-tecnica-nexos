import { Route } from "@angular/router";

export const productsRoutes :Route[] = [
    {
        path:'list',
        loadComponent: () => import('./pages/products-list-page/products-list-page').then(m => m.ProductsListPage)
    },
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'list'
    }
]