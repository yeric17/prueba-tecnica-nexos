import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { OrdersService } from '../../services/orders.service';
import { OrderCard } from '../../components/order-card/order-card';
import { Order, PayOrderRequest } from '../../models/orders.model';

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

  protected payOrder(order: Order): void {
    const payOrderRequest = this.buildPayOrderRequest(order);
    this.ordersService.payOrder(payOrderRequest).subscribe({
      next: () => {
        this.orders.reload();
      }
    })
  }

  private buildPayOrderRequest(order: Order): PayOrderRequest {
    return {
      orderId: order.id,
      amount: order.totalAmount,
      currency: 'COP', 
      paymentMethod: 'CreditCard' 
    } as PayOrderRequest;
  }
}
