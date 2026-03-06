import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { Product } from '../../data/products.data';
import { ProductImagePipe } from '../../pipes/product-image.pipe';
import { LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-product-card',
  imports: [CurrencyPipe, ProductImagePipe, LucideAngularModule],
  templateUrl: './product-card.html',
  styleUrl: './product-card.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductCard {
  product = input.required<Product>();
  addToCart = output<Product>();

  protected handleAddToCart(): void {
    this.addToCart.emit(this.product());
  }
}
