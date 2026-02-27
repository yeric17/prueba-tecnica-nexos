import { Route } from "@angular/router";

export const ordersRoutes: Route[] = [
    {
        path: '',
        loadComponent: () => import('../../shared/layouts/base-layout/base-layout').then(m => m.BaseLayout),
        children: [
            {
                path: '',
                loadComponent: () => import('./pages/orders-list-page/orders-list-page').then(m => m.OrdersListPage)
            }
        ]
    }
];
