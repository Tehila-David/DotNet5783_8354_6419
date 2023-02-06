
namespace BO
{
  public class Order
    {
        /// <summary>
        /// ID of Order
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Customer name of order
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// Customer email of order
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// Customer address of order
        /// </summary>
        public string? CustomerAddress { get; set; }
        /// <summary>
        /// Status of order
        /// </summary>
        public OrderStatus? Status { get; set; }
        /// <summary>
        ///  Order Date of order
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// Shipping Date of order
        /// </summary>
        public DateTime? ShipDate { get; set; }
        /// <summary>
        /// Delivery date of Order
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// list of Order Items
        /// </summary>
        public List <OrderItem?>? Items { get; set; }
        /// <summary>
        /// Total price of order
        /// </summary>
        public double TotalPrice { get; set; }


        public override string ToString()
        {
            string str = "";
            str += $"ID: {ID} \n";
            str += $"Costumer Name: {CustomerName} \n";
            str += $"CustomerEmail: {CustomerEmail} \n";
            str += $"CustomerAddress: {CustomerAddress} \n";
            str += $"OrderDate: {OrderDate}\n";
            str += $"Ship Date: {ShipDate}\n";
            str += $"Delivery Date: {DeliveryDate} \n";
            str += $"Total Price: {TotalPrice} \n";
            str += $"Items:  \n";

            foreach (OrderItem? item in Items)
            {
                str += $"OrderItem: {item} \n";
            } 
            return str;
        }
    }
}
