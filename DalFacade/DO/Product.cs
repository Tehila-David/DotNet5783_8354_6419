

namespace DO;

/// <summary>
/// structure for Product
/// </summary>
public struct Product
{
    /// <summary>
    /// Unique ID of Prudct
    /// </summary>
    public int ID ;
    /// <summary>
    /// Name of product
    /// </summary>
    public string Name;
    /// <summary>
    /// Price of  Product
    /// </summary>
    public double Price;
    /// <summary>
    /// Category of product
    /// </summary>
    Category Category;
    /// <summary>
    /// Instock of product
    /// </summary>
    public int InStock;

    public override string ToString() => $@"
    Product ID={ID}: {Name}, 
    category - {Category}
    Price: {Price}
    Amount in stock: {InStock}";
}
