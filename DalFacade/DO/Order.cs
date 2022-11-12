

using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

public struct Order
{
    public int ID;
    public string CostumerName;
    public string CustomerEmail;
    public string CustomerAdress;
    public DateTime OrderDate;
    public DateTime ShipDate;
    public DateTime DeliveryDate;

    public override string ToString() => $@"
    ID:{ID},
    Costumer Name: {CostumerName},
    Customer Email: {CustomerEmail},
    Customer Address: {CustomerAdress},
    OrderDate: {OrderDate},
    Ship Date: {ShipDate},
    Delivery Date: {DeliveryDate}";




}
