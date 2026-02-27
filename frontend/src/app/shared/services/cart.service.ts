import { computed, effect, inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Product } from '../../modules/products/data/products.data';

export interface CartItem {
  productId: string;
  productName: string;
  unitPrice: number;
  quantity: number;
  imageUrl: string;
}

export interface OrderRequest {
  userId: string;
  shippingAddress: string;
  shippingCity: string;
  shippingCountry: string;
  items: OrderItemRequest[];
}

export interface OrderItemRequest {
    productName: string;
    quantity: number;
    unitPrice: number;
}

export interface ShippingInfo {
  address: string;
  city: string;
  country: string;
}

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private readonly http = inject(HttpClient);
  private readonly API_HOST = environment.apiHost;
  private readonly CART_STORAGE_KEY = 'shopping_cart';

  private readonly _items = signal<CartItem[]>(this.loadCartFromStorage());

  public readonly items = this._items.asReadonly();

  public readonly itemCount = computed(() => {
    return this._items().reduce((total, item) => total + item.quantity, 0);
  });

  public readonly totalAmount = computed(() => {
    return this._items().reduce((total, item) => total + item.unitPrice * item.quantity, 0);
  });

  public readonly isEmpty = computed(() => this._items().length === 0);

  constructor() {
    // Auto-save cart to localStorage whenever it changes
    effect(() => {
      this.saveCartToStorage();
      this._items(); // Track the signal
    });
  }

  addToCart(product: Product, quantity: number = 1): void {
    const currentItems = this._items();
    const existingItemIndex = currentItems.findIndex(item => item.productId === product.id);

    if (existingItemIndex !== -1) {
      // Item exists, update quantity
      const updatedItems = [...currentItems];
      updatedItems[existingItemIndex] = {
        ...updatedItems[existingItemIndex],
        quantity: updatedItems[existingItemIndex].quantity + quantity
      };
      this._items.set(updatedItems);
    } else {
      // Add new item
      const newItem: CartItem = {
        productId: product.id,
        productName: product.name,
        unitPrice: product.price,
        quantity: quantity,
        imageUrl: product.imageUrl
      };
      this._items.set([...currentItems, newItem]);
    }
  }

  removeFromCart(productId: string): void {
    const updatedItems = this._items().filter(item => item.productId !== productId);
    this._items.set(updatedItems);
  }

  updateQuantity(productId: string, quantity: number): void {
    if (quantity <= 0) {
      this.removeFromCart(productId);
      return;
    }

    const currentItems = this._items();
    const itemIndex = currentItems.findIndex(item => item.productId === productId);

    if (itemIndex !== -1) {
      const updatedItems = [...currentItems];
      updatedItems[itemIndex] = {
        ...updatedItems[itemIndex],
        quantity: quantity
      };
      this._items.set(updatedItems);
    }
  }

  clearCart(): void {
    this._items.set([]);
  }

  getItemQuantity(productId: string): number {
    const item = this._items().find(item => item.productId === productId);
    return item?.quantity ?? 0;
  }

  buildOrderRequest(userId: string, shippingInfo: ShippingInfo): OrderRequest {
    return {
      userId: userId,
      shippingAddress: shippingInfo.address,
      shippingCity: shippingInfo.city,
      shippingCountry: shippingInfo.country,
      items: this._items().map(item => ({
        productName: item.productName,
        quantity: item.quantity,
        unitPrice: item.unitPrice
      }))
    };
  }

  submitOrder(userId: string, shippingInfo: ShippingInfo) {
    const orderRequest = this.buildOrderRequest(userId, shippingInfo);
    return this.http.post(`${this.API_HOST}/orders-service/orders`, orderRequest);
  }

  private saveCartToStorage(): void {
    if (typeof window !== 'undefined' && window.localStorage) {
      localStorage.setItem(this.CART_STORAGE_KEY, JSON.stringify(this._items()));
    }
  }

  private loadCartFromStorage(): CartItem[] {
    if (typeof window !== 'undefined' && window.localStorage) {
      const stored = localStorage.getItem(this.CART_STORAGE_KEY);
      if (stored) {
        try {
          return JSON.parse(stored);
        } catch (error) {
          console.error('Error loading cart from storage:', error);
          return [];
        }
      }
    }
    return [];
  }
}
