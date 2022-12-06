
using BlApi;
using DalApi;
using Dal;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Runtime.ConstrainedExecution;

namespace BlImplementation;

internal class Cart:ICart
{
    IDal Dal = new DalList();
    //This function updates the amount of a product in the cart
    public BO.Cart UpdateProductAmount(BO.Cart myCart, int productId, int newAmount)
    {
        DO.Product doProduct;
        try //Checking if the productId exists 
        {
            doProduct = Dal.Product.GetById(productId);
        }
        catch (DO.NotExists ex)
        {
            throw new BO.EntityNotExist("Product ID is not valid", ex);
        }
        //Checking if product exists in cart
        BO.OrderItem? newItem = myCart.Items?.FirstOrDefault(item => item?.ProductID == productId);
        if(newAmount > newItem?.Amount)
        {
            if (++newItem.Amount > doProduct.InStock)
            {
                throw new BO.notEnoughInStock("There are not enough product items in stock");
            }
            if (newItem != null)
            {
               myCart.Items?.Add(newItem);
            }
    }
        else if (newAmount < newItem?.Amount)
        {
            int amountDiff = newAmount - newItem.Amount;    
            newItem.Amount = newAmount;
            newItem.TotalPrice = newAmount * newItem.Price;
            myCart.TotalPrice -= amountDiff * newItem.Price;
        }
        else if(newAmount ==  0)
        {
            myCart.TotalPrice -= newItem?.Amount * newItem?.Price;
            myCart.Items?.Remove(newItem);
        }
        return myCart;
    }

    public BO.Cart AddProduct(BO.Cart myCart, int productId)
    {
        DO.Product doProduct;

        try //Checking if the productId exists 
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
        if(itemExists == true) // Checking if after adding to the amount of a product which already exists in the
                               // cart if it would exceed the amount of the product in stock
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

    public void UpdateTotalSum(BO.Cart myCart)
    {
        DO.Product doProduct;

        if (String.IsNullOrEmpty(myCart.CustomerName))
            throw new BO.IncorrectName("Customer name is empty");
        if (myCart.CustomerAddress == null)
            throw new BO.IncorrectAddress("Customer address is empty");
        Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
        if (myCart.CustomerEmail == null || (emailRegex.IsMatch(myCart.CustomerEmail)) == false)
            throw new BO.IncorrectEmailAddress("Customer email address is empty or not in correct format");
        foreach(var item in myCart.Items)
        {
            try //Checking if the productId exists 
            {
                doProduct = Dal.Product.GetById(item.ProductID);
            }
            catch (DO.NotExists ex)
            {
                throw new BO.EntityNotExist("Product ID is not valid", ex);
            }
            if (item.Amount <= 0)
                throw new BO.negativeProductAmount("The product item amount is negative");
            if (item.Amount > doProduct.InStock)
                throw new BO.notEnoughInStock("There are not enough product items in stock");
        }
        DO.Order doOrder = new DO.Order()
        {
            //ID = Config.NextOrderID,
            CustomerName = myCart.CustomerName,
            CustomerEmail = myCart.CustomerEmail,
            CustomerAddress = myCart.CustomerAddress,
            OrderDate = DateTime.Now,
            ShipDate = DateTime.MinValue,
            DeliveryDate = DateTime.MinValue
        };
        int orderID = Dal.Order.Add(doOrder);
        foreach (var item in myCart.Items)
        {
            DO.OrderItem doOrderItem = new DO.OrderItem()
            {
                ID = item.ID,
                OrderID = orderID,
                ProductID = item.ProductID, 
                Price = (double)item.Price,
                Amount = item.Amount
            };
        }
        DO.Product myProduct;
        foreach (var item in myCart.Items)
        {
            try //Checking if the productId exists 
            {
                myProduct = Dal.Product.GetById(item.ProductID);
            }
            catch (DO.NotExists ex)
            {
                throw new BO.EntityNotExist("Product ID is not valid", ex);
            }
            myProduct.InStock -= item.Amount;

        }

    }

}
