import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from '../../data/products.data';
import { LucideAngularModule } from 'lucide-angular';
import { Button } from '../../../../shared/components/buttons/button/button';

@Component({
  selector: 'app-products-manager-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, LucideAngularModule, Button],
  templateUrl: './products-manager-page.html',
  styleUrl: './products-manager-page.css',
  providers: [ProductService]
})
export class ProductsManagerPage implements OnInit {
  
  private productService = inject(ProductService);
  private fb = inject(FormBuilder);
  private router = inject(Router);

  products = signal<Product[]>([]);
  isLoading = signal(false);
  showForm = signal(false);
  editingProduct = signal<Product | null>(null);
  selectedProductForImages = signal<Product | null>(null);
  productImages = signal<any[]>([]);

  productForm: FormGroup;

  constructor() {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
      price: [0, [Validators.required, Validators.min(0)]],
      category: [''],
      stockQuantity: [0, [Validators.required, Validators.min(0)]],
      imageUrl: [''],
      isActive: [true]
    });
  }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.isLoading.set(true);
    this.productService.getProducts().subscribe({
      next: (products) => {
        this.products.set(products);
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Error loading products:', error);
        this.isLoading.set(false);
      }
    });
  }

  openCreateForm(): void {
    this.editingProduct.set(null);
    this.productForm.reset({
      name: '',
      description: '',
      price: 0,
      category: '',
      stockQuantity: 0,
      imageUrl: '',
      isActive: true
    });
    this.showForm.set(true);
  }

  openEditForm(product: Product): void {
    this.editingProduct.set(product);
    this.productForm.patchValue({
      name: product.name,
      description: product.description || '',
      price: product.price,
      category: product.category || '',
      stockQuantity: product.stockQuantity,
      imageUrl: product.imageUrl || '',
      isActive: product.isActive
    });
    this.showForm.set(true);
  }

  cancelForm(): void {
    this.showForm.set(false);
    this.editingProduct.set(null);
    this.productForm.reset();
  }

  saveProduct(): void {
    if (this.productForm.invalid) {
      return;
    }

    this.isLoading.set(true);
    const formValue = this.productForm.value;

    if (this.editingProduct()) {
      // Update
      const request = {
        productId: this.editingProduct()!.id,
        ...formValue
      };
      
      this.productService.updateProduct(request).subscribe({
        next: () => {
          this.isLoading.set(false);
          this.showForm.set(false);
          this.loadProducts();
        },
        error: (error) => {
          console.error('Error updating product:', error);
          this.isLoading.set(false);
          alert('Error al actualizar el producto');
        }
      });
    } else {
      // Create
      this.productService.createProduct(formValue).subscribe({
        next: () => {
          this.isLoading.set(false);
          this.showForm.set(false);
          this.loadProducts();
        },
        error: (error) => {
          console.error('Error creating product:', error);
          this.isLoading.set(false);
          alert('Error al crear el producto');
        }
      });
    }
  }

  deleteProduct(product: Product): void {
    if (!confirm(`¿Estás seguro de eliminar el producto "${product.name}"?`)) {
      return;
    }

    this.isLoading.set(true);
    this.productService.deleteProduct(product.id).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.loadProducts();
      },
      error: (error) => {
        console.error('Error deleting product:', error);
        this.isLoading.set(false);
        alert('Error al eliminar el producto');
      }
    });
  }

  toggleProductStatus(product: Product): void {
    const request = {
      productId: product.id,
      name: product.name,
      description: product.description,
      price: product.price,
      category: product.category,
      stockQuantity: product.stockQuantity,
      imageUrl: product.imageUrl,
      isActive: !product.isActive
    };

    this.productService.updateProduct(request).subscribe({
      next: () => {
        this.loadProducts();
      },
      error: (error) => {
        console.error('Error toggling product status:', error);
        alert('Error al cambiar el estado del producto');
      }
    });
  }

  openImageManager(product: Product): void {
    this.selectedProductForImages.set(product);
    this.loadProductImages(product.id);
  }

  closeImageManager(): void {
    this.selectedProductForImages.set(null);
    this.productImages.set([]);
  }

  loadProductImages(productId: number): void {
    this.productService.getProductImages(productId).subscribe({
      next: (images) => {
        this.productImages.set(images);
      },
      error: (error) => {
        console.error('Error loading images:', error);
      }
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length === 0) {
      return;
    }

    const file = input.files[0];
    const product = this.selectedProductForImages();
    
    if (!product) {
      return;
    }

    const isPrimary = this.productImages().length === 0;

    this.productService.uploadProductImage(product.id, file, isPrimary).subscribe({
      next: () => {
        this.loadProductImages(product.id);
        input.value = '';
      },
      error: (error) => {
        console.error('Error uploading image:', error);
        alert('Error al subir la imagen');
      }
    });
  }

  deleteImage(imageId: string): void {
    if (!confirm('¿Estás seguro de eliminar esta imagen?')) {
      return;
    }

    this.productService.deleteProductImage(imageId).subscribe({
      next: () => {
        const product = this.selectedProductForImages();
        if (product) {
          this.loadProductImages(product.id);
        }
      },
      error: (error) => {
        console.error('Error deleting image:', error);
        alert('Error al eliminar la imagen');
      }
    });
  }

  getImageUrl(filePath: string): string {
    return this.productService.getImageUrl(filePath);
  }

  getProductImageUrl(productId: number): string {
    return this.productService.getProductImageUrl(productId);
  }

  goToProducts(): void {
    this.router.navigate(['/products/list']);
  }
}
