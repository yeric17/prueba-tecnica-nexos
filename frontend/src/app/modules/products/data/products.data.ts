export interface Product {
    id: string;
    name: string;
    imageUrl: string;
    price: number;
}

export const PRODUCTS: Product[] = [
    {
        id: 'p-avocados-pack',
        name: 'Paquete de aguacates Hass (4 pzas)',
        imageUrl: '/images/products/avocados-pack.jpg',
        price: 5.99
    },
    {
        id: 'p-granola-jar',
        name: 'Granola artesanal con miel 500g',
        imageUrl: '/images/products/granola-jar.jpg',
        price: 7.5
    },
    {
        id: 'p-coffee-beans',
        name: 'Café en grano tostado 250g',
        imageUrl: '/images/products/coffee-beans.png',
        price: 8.25
    },
    {
        id: 'p-olive-oil',
        name: 'Aceite de oliva extra virgen 500ml',
        imageUrl: '/images/products/olive-oil.jpg',
        price: 12.3
    },
    {
        id: 'p-sourdough-bread',
        name: 'Pan sourdough artesanal',
        imageUrl: '/images/products/sourdough-bread.jpg',
        price: 4.75
    }
];