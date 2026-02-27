import { ChangeDetectionStrategy, Component, computed, inject, viewChild } from '@angular/core';
import { Menu, MenuContent, MenuTrigger } from '@angular/aria/menu';
import { OverlayModule } from '@angular/cdk/overlay';
import { CurrencyPipe } from '@angular/common';
import { CartService } from '../../services/cart.service';
import { CartItemComponent } from './components/cart-item/cart-item';

@Component({
  selector: 'app-user-cart',
  imports: [Menu, MenuContent, MenuTrigger, OverlayModule, CurrencyPipe, CartItemComponent],
  templateUrl: './user-cart.html',
  styleUrl: './user-cart.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserCart {
  private readonly cartService = inject(CartService);

  protected readonly items = this.cartService.items;
  protected readonly itemCount = this.cartService.itemCount;
  protected readonly totalAmount = this.cartService.totalAmount;
  protected readonly isEmpty = this.cartService.isEmpty;

  cartMenu = viewChild<Menu<string>>('cartMenu');

  protected handleRemoveItem(productId: string): void {
    this.cartService.removeFromCart(productId);
  }

  protected handleIncreaseQuantity(productId: string): void {
    const currentQuantity = this.cartService.getItemQuantity(productId);
    this.cartService.updateQuantity(productId, currentQuantity + 1);
  }

  protected handleDecreaseQuantity(productId: string): void {
    const currentQuantity = this.cartService.getItemQuantity(productId);
    this.cartService.updateQuantity(productId, currentQuantity - 1);
  }

  protected handleClearCart(): void {
    if (confirm('¿Estás seguro de que deseas vaciar el carrito?')) {
      this.cartService.clearCart();
    }
  }
}
