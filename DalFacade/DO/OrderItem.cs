

using System.Xml.Linq;

namespace DO;
/// <summary>
/// struct for order item
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Unique ID of Order Item
    /// </summary>
    public int ID;
    /// <summary>
    ///  ID of item
    /// </summary>
    public int ProductID;
    /// <summary>
    /// ID of Order
    /// </summary>
    public int OrderID;
    /// <summary>
    /// Price of item
    /// </summary>
    public int Price;
    /// <summary>
    /// Amount of items in order
    /// </summary>
    public int Amount;

    public override string ToString() => $@"
    ID:{ID},
    Order ID: {OrderID},
    Product Id: {ProductID},
    Price per product: {Price},
    Amount: {Amount}";

}
