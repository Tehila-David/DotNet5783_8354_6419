


namespace DO;
/// <summary>
/// struct for order item
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Unique ID of Order Item
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    ///  ID of item
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// ID of Order
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// Price of item
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Amount of items in order
    /// </summary>
    public int Amount { get; set; }



    public override string ToString() => $@"
ID:{ID},
Order ID: {OrderID},
Product Id: {ProductID},
Price per product: {Price},
Amount: {Amount}
";

}
