
namespace DO;
/// <summary>
/// struct for final order
/// </summary>
public struct Order
{
    /// <summary>
    /// Unique ID of Order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// name of costumer
    /// </summary>
    public string? CustomerName{ get; set; }
    /// <summary>
    /// Email of costumer
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// address of costumer
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// Date of Order
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// Date of shipping
    /// </summary>
    public DateTime? ShipDate { get ; set ; }
    /// <summary>
    /// Date of  Delivery
    /// </summary>
    public DateTime? DeliveryDate { get ; set ;}


    public override string ToString() => $@"
ID:{ID},
Costumer Name: {CustomerName},
Customer Email: {CustomerEmail},
Customer Address: {CustomerAddress},
OrderDate: {OrderDate},
Ship Date: {ShipDate},
Delivery Date: {DeliveryDate}";


}
