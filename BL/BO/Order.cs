
namespace BO
{
  public class Order
    {
        public int ID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List <OrderItem>? Items { get; set; }
        public double TotalPrice { get; set; }

        
        public override string ToString()
        {
            string str = "";
            str += $"ID: {ID} \n";
            str += $"Costumer Name: {CustomerName} \n";
            str += $"CustomerEmail: {CustomerEmail} \n";
            str += $"CustomerAddress: {CustomerAddress} \n";
            str += $"OrderDate: {OrderDate}";
            str += $"Ship Date: {ShipDate}";
            str += $"Delivery Date: {DeliveryDate}";
            str += $"Total Price: {TotalPrice} \n";
            str += $"Items:  \n";

            foreach (var item in Items)
            {
                str += $"OrderItem: {item} \n";
            }
            return str;
        }
    }
}
