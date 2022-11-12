
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
    public string? CostumerName{ get; set; }
    /// <summary>
    /// Email of costumer
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// address of costumer
    /// </summary>
    public string? CustomerAdress { get; set; }
    /// <summary>
    /// Date of Order
    /// </summary>
    public TimeSpan? OrderDate { get; set; }
    /// <summary>
    /// Date of shipping
    /// </summary>
    public DateTime? ShipDate { get ;  set ; }
    /// <summary>
    /// Date of  Delivery
    /// </summary>
    public DateTime? DeliveryDate { get ; set ;}


    public override string ToString() => $@"
    ID:{ID},
    Costumer Name: {CostumerName},
    Customer Email: {CustomerEmail},
    Customer Address: {CustomerAdress},
    OrderDate: {OrderDate},
    Ship Date: {ShipDate},
    Delivery Date: {DeliveryDate}";


}
