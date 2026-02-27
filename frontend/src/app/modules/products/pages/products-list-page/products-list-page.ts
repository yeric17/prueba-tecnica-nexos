import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { rxResource } from '@angular/core/rxjs-interop';
import { ProductCard } from '../../components/product-card/product-card';
import { Product } from '../../data/products.data';
import { CartService } from '../../../../shared/services/cart.service';

@Component({
  selector: 'app-products-list-page',
  imports: [ProductCard],
  templateUrl: './products-list-page.html',
  styleUrl: './products-list-page.css',
  providers: [ProductService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductsListPage {
  private readonly productService = inject(ProductService);
  private readonly cartService = inject(CartService);

  products = rxResource({
    stream: () => this.productService.getProducts(),
    defaultValue: []
  });

  protected handleAddToCart(product: Product): void {
    this.cartService.addToCart(product);
  }
}
