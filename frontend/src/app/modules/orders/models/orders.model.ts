
export interface Order {
    id: string;
    orderNumber: string;
    userId: string;
    totalAmount: number;
    status: 'Pending' | 'Shipped' | 'Delivered' | 'Cancelled';
    createdAt: string;
    items: OrderItem[];
    shippingAddress: string;
}

export interface OrderItem {
    productName: string;
    quantity: number;
    unitPrice: number;
}