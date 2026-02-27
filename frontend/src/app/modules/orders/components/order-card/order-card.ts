import { ChangeDetectionStrategy, Component, computed, inject, input, output } from '@angular/core';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { Order } from '../../models/orders.model';
import { ProductService } from '../../../products/services/product.service';
import { PRODUCTS } from '../../../products/data/products.data';

@Component({
  selector: 'app-order-card',
  imports: [CurrencyPipe, DatePipe],
  templateUrl: './order-card.html',
  styleUrl: './order-card.css',
  providers: [ProductService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class OrderCard {
  order = input.required<Order>();
  payOrder = output<Order>();

  protected readonly itemsWithImages = computed(() => {
    return this.order().items.map(item => {
      const product = PRODUCTS.find(p => p.name === item.productName);
      return {
        ...item,
        imageUrl: product?.imageUrl ?? '/images/products/placeholder.jpg',
        subtotal: item.quantity * item.unitPrice
      };
    });
  });

  protected readonly statusConfig = computed(() => {
    const status = this.order().status;
    const configs = {
      'Pending': { label: 'Pendiente', class: 'status--pending', icon: 'schedule' },
      'Shipped': { label: 'Enviado', class: 'status--shipped', icon: 'local_shipping' },
      'Delivered': { label: 'Entregado', class: 'status--delivered', icon: 'check_circle' },
      'Cancelled': { label: 'Cancelado', class: 'status--cancelled', icon: 'cancel' }
    };
    return configs[status];
  });

  protected handlePay(): void {
    this.payOrder.emit(this.order());
  }
}
