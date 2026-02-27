import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { CartItem } from '../../../../services/cart.service';

@Component({
  selector: 'app-cart-item',
  imports: [CurrencyPipe],
  templateUrl: './cart-item.html',
  styleUrl: './cart-item.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CartItemComponent {
  item = input.required<CartItem>();
  
  removeItem = output<string>();
  increaseQuantity = output<string>();
  decreaseQuantity = output<string>();

  protected handleRemove(): void {
    this.removeItem.emit(this.item().productId);
  }

  protected handleIncrease(): void {
    this.increaseQuantity.emit(this.item().productId);
  }

  protected handleDecrease(): void {
    this.decreaseQuantity.emit(this.item().productId);
  }
}
