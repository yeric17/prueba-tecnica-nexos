namespace Domain.Orders
{
    public class OrderItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal => Quantity * UnitPrice;

        #region Navigation Properties

        public Order Order { get; set; } = null!;

        #endregion

        private OrderItem() { }

        public OrderItem(Guid orderId, string productName, int quantity, decimal unitPrice)
        {
            OrderId = orderId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
