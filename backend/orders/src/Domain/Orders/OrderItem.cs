namespace Domain.Orders
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public string ProductName { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Subtotal { get; private set; }

        #region Navigation Properties

        public Order Order { get; private set; } = null!;

        #endregion

        private OrderItem() { }

        public OrderItem(Guid orderId, string productName, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;

            Subtotal = quantity * unitPrice;
        }
    }
}
