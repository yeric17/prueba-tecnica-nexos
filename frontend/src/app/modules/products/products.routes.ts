import { Route } from "@angular/router";

export const productsRoutes :Route[] = [
    {
        path:'',
        loadComponent: () => import('../../shared/layouts/base-layout/base-layout').then(m => m.BaseLayout),
        children: [
            {
                path: 'list',
                loadComponent: () => import('./pages/products-list-page/products-list-page').then(m => m.ProductsListPage)
            }
        ]
    },
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'list'
    }
]