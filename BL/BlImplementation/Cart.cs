
using BlApi;
using DalApi;
using Dal;
using System.Reflection.Metadata;

namespace BlImplementation;

internal class Cart:ICart
{
    IDal Dal = new DalList();
    public void UpdateTotalSum(BO.Cart myCart)
    {
        
    }
    
    public BO.Cart AddProduct(BO.Cart myCart, int productId)
    {
        DO.Product doProduct;

        try //Checking if the productId exists in stock
        {
            doProduct = Dal.Product.GetById(productId);
        }
        catch(DO.NotExists ex)
        {
            throw new BO.EntityNotExist("Product ID is not valid", ex);
        }
        //Checking if product exists in cart
        BO.OrderItem? newItem = myCart.Items?.FirstOrDefault(item => item?.ProductID == productId);
        
        bool itemExists = false;

        if (newItem != null)
        {
            itemExists = true;
        }
        if(newItem == null) //If the product does not exist in the cart
        {
            newItem = new()
            {
               ID = 0,
               Name = doProduct.Name,
               Price = doProduct.Price,
               Amount = 0,
               ProductID = productId,
               TotalPrice = doProduct.Price
            };
        }
        if(itemExists == true)
        {
            if(++newItem.Amount > doProduct.InStock)
            {
                throw new BO.notEnoughInStock("There are not enough product items in stock");
            }
        }

        

        if(newItem != null)
        {
            myCart.Items?.Add(newItem);
        }
        return myCart;

    }
}
