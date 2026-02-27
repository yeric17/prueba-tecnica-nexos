import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { rxResource } from '@angular/core/rxjs-interop';
import { ProductCard } from '../../components/product-card/product-card';
import { Product } from '../../data/products.data';

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

  products = rxResource({
    stream: () => this.productService.getProducts(),
    defaultValue: []
  });

  protected handleAddToCart(product: Product): void {
    console.log('Producto agregado al carrito:', product);
    // Aquí se implementará la lógica para agregar al carrito
  }
}
