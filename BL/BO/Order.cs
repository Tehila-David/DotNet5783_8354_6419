
namespace BO
{
  public class Order
    {
        public int ID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List <OrderItem>? Items { get; set; }
        public double TotalPrice { get; set; }

        public override string ToString() => $@"
        ID:{ID},
        Costumer Name: {CustomerName},
        Customer Email: {CustomerEmail},
        Customer Address: {CustomerAddress},
        OrderDate: {OrderDate},
        Ship Date: {ShipDate},
        Delivery Date: {DeliveryDate}
        Total Price: {TotalPrice}";
    }
}
