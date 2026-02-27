import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { rxResource } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-products-list-page',
  imports: [],
  templateUrl: './products-list-page.html',
  styleUrl: './products-list-page.css',
  providers: [ProductService]
})
export class ProductsListPage {
  private readonly productService = inject(ProductService)

  products = rxResource({
    stream: () => this.productService.getProducts(),
    defaultValue: []
  })

}
