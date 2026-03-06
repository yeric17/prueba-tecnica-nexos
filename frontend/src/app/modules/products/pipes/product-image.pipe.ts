import { inject, Pipe, PipeTransform } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Pipe({
  name: 'productImage',
  standalone: true
})
export class ProductImagePipe implements PipeTransform {
  private API_HOST = environment.apiHost;

  transform(productId: number): string {
    return `${this.API_HOST}/orders-service/products/${productId}/image`;
  }
}
