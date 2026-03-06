export interface Product {
    id: number;
    name: string;
    description?: string;
    price: number;
    category?: string;
    stockQuantity: number;
    imageUrl?: string;
    isActive: boolean;
    createdAt?: string;
    updatedAt?: string;
    images?: ProductImage[];
}

export interface ProductImage {
    id: string;
    productId: number;
    fileName: string;
    filePath: string;
    contentType: string;
    fileSize: number;
    isPrimary: boolean;
    createdAt: string;
}

export interface CreateProductRequest {
    name: string;
    description?: string;
    price: number;
    category?: string;
    stockQuantity: number;
    imageUrl?: string;
}

export interface UpdateProductRequest {
    productId: number;
    name: string;
    description?: string;
    price: number;
    category?: string;
    stockQuantity: number;
    imageUrl?: string;
    isActive: boolean;
}

// Datos de ejemplo - mantener compatibilidad con código existente
export const PRODUCTS: Product[] = [
    {
        id: 1,
        name: 'Paquete de aguacates Hass (4 pzas)',
        imageUrl: '/images/products/avocados-pack.jpg',
        price: 12000,
        stockQuantity: 50,
        isActive: true
    },
    {
        id: 2,
        name: 'Granola artesanal con miel 500g',
        imageUrl: '/images/products/granola-jar.jpg',
        price: 15000,
        stockQuantity: 30,
        isActive: true
    },
    {
        id: 3,
        name: 'Café en grano tostado 250g',
        imageUrl: '/images/products/coffee-beans.png',
        price: 23000,
        stockQuantity: 40,
        isActive: true
    },
    {
        id: 4,
        name: 'Aceite de oliva extra virgen 500ml',
        imageUrl: '/images/products/olive-oil.jpg',
        price: 18000,
        stockQuantity: 25,
        isActive: true
    },
    {
        id: 5,
        name: 'Pan sourdough artesanal',
        imageUrl: '/images/products/sourdough-bread.png',
        price: 4750,
        stockQuantity: 15,
        isActive: true
    }
];