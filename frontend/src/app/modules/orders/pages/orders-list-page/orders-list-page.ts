import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { OrdersService } from '../../services/orders.service';
import { OrderCard } from '../../components/order-card/order-card';
import { Order } from '../../models/orders.model';

@Component({
  selector: 'app-orders-list-page',
  imports: [RouterLink, OrderCard],
  templateUrl: './orders-list-page.html',
  styleUrl: './orders-list-page.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class OrdersListPage {
  private readonly ordersService = inject(OrdersService);
  
  protected orders = rxResource({
    stream: () => this.ordersService.getOrders(),
    defaultValue: []
  });

  protected handlePayOrder(order: Order): void {
    console.log('Procesando pago para orden:', order);
    // Aquí se implementará la lógica para procesar el pago
  }
}
