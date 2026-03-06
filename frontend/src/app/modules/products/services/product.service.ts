import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { environment } from "../../../../environments/environment";
import { CreateProductRequest, Product, ProductImage, UpdateProductRequest, PRODUCTS } from "../data/products.data";


@Injectable()
export class ProductService {
    
    private API_HOST = environment.apiHost;
    private http = inject(HttpClient);

    public getProducts(): Observable<Product[]> {
        return this.http.get<Product[]>(`${this.API_HOST}/orders-service/products`);
    }

    public getProductById(id: number): Observable<Product> {
        return this.http.get<Product>(`${this.API_HOST}/orders-service/products/${id}`);
    }

    public createProduct(request: CreateProductRequest): Observable<{ id: number }> {
        return this.http.post<{ id: number }>(`${this.API_HOST}/orders-service/products`, request);
    }

    public updateProduct(request: UpdateProductRequest): Observable<void> {
        return this.http.put<void>(`${this.API_HOST}/orders-service/products/${request.productId}`, request);
    }

    public deleteProduct(id: number): Observable<void> {
        return this.http.delete<void>(`${this.API_HOST}/orders-service/products/${id}`);
    }

    // Image methods
    public uploadProductImage(productId: number, file: File, isPrimary: boolean = false): Observable<{ id: string }> {
        const formData = new FormData();
        formData.append('file', file);
        formData.append('isPrimary', isPrimary.toString());
        
        return this.http.post<{ id: string }>(
            `${this.API_HOST}/orders-service/products/${productId}/images`,
            formData
        );
    }

    public getProductImages(productId: number): Observable<ProductImage[]> {
        return this.http.get<ProductImage[]>(`${this.API_HOST}/orders-service/products/${productId}/images`);
    }

    public deleteProductImage(imageId: string): Observable<void> {
        return this.http.delete<void>(`${this.API_HOST}/orders-service/products/images/${imageId}`);
    }

    public getImageUrl(filePath: string): string {
        return `${this.API_HOST}/orders-service/files/${filePath}`;
    }

    public getProductImageUrl(productId: number): string {
        return `${this.API_HOST}/orders-service/products/${productId}/image`;
    }
}