
using BlApi;
using DalApi;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Runtime.ConstrainedExecution;
using System;

namespace BlImplementation;

internal class Cart:ICart
{
    DalApi.IDal? dal = DalApi.Factory.Get();
    //This function updates the amount of a product in the cart

    public IEnumerable<BO.OrderItem> cartItems(BO.Cart myCart)
    {
        return myCart.Items;
    }

    public BO.Cart UpdateProductAmount(BO.Cart myCart, int productId, int newAmount)
    {
        DO.Product doProduct;
        try //Checking if the productId exists in the product list 
        {
            doProduct = dal?.Product.GetById(productId)
             ?? throw new BO.InternalProblem("Dal layer is inaccessible");

        }
        catch (DO.NotExists ex)
        {
            throw new BO.EntityNotExist("Product ID is not valid", ex);
        }
        //Checking if product exists in cart
        BO.OrderItem? newItem = myCart.Items?.FirstOrDefault(item => item?.ProductID == productId);
        if (newItem != null)
        {
            if (newAmount > newItem?.Amount)// Checking if the amount grew
            {
                //adding amount of products which exists in the cart to the cart
                if(newAmount <= doProduct.InStock)
                {
                    newItem.Amount = newAmount;
                    double oldTotalPrice = newItem.TotalPrice;
                    newItem.TotalPrice = newItem.Price* newAmount;
                    myCart.TotalPrice += newItem.TotalPrice - oldTotalPrice;
                }
                else
                {
                    throw new BO.notEnoughInStock("There are not enough product items in stock");
                }
            }
            else if (newAmount < newItem?.Amount)
            {
                int amountDiff = newItem.Amount - newAmount;
                newItem.Amount = newAmount;
                newItem.TotalPrice = newAmount * newItem.Price;
                myCart.TotalPrice -= amountDiff * newItem.Price;
            }
            else if (newAmount == 0)
            {
                myCart.Items?.Remove(newItem);
                myCart.TotalPrice -= newItem.Amount * newItem.Price;
            }
        }
        return myCart;
    }

    public BO.Cart AddProduct(BO.Cart myCart, int productId)
    {
        if(myCart.Items == null)
        {
            myCart.Items = new();
        }
        DO.Product doProduct;

        try //Checking if the productId exists within the list of products
        {
            doProduct = dal?.Product.GetById(productId)
            ?? throw new BO.InternalProblem("Dal layer is inaccessible");
        }
        catch(DO.NotExists ex)
        {
            throw new BO.EntityNotExist("Product ID is not valid", ex);
        }
        //Checking if product exists in cart
        BO.OrderItem? newItem = myCart.Items?.FirstOrDefault(item => item?.ProductID == productId);

           if (newItem == null) //If the product does not exist in the cart
           {
            if (doProduct.InStock > 0) // Checking if there is enough of the product in stock
            {
                newItem = new()
                {
                    ID = 0,
                    Name = doProduct.Name,
                    Price = doProduct.Price,
                    Amount = 1,
                    ProductID = productId,
                    TotalPrice = doProduct.Price
                };
                myCart.TotalPrice += doProduct.Price;
                myCart.Items?.Add(newItem);
            }
           }
        //If the product already exists in the cart
        // Checking if the amount of a product which already exists in the
        // cart is less than the amount of the product in stock (so that it is possible to add)
        else
        {
            if (newItem.Amount < doProduct.InStock)
            {
                newItem.Amount++;
                newItem.TotalPrice += doProduct.Price;
                myCart.TotalPrice += doProduct.Price;
            }
            else
            {
                throw new BO.notEnoughInStock("There are not enough product items in stock");
            }
        }
        return myCart;
    }

    public void CartConfirmation(BO.Cart myCart)
    {
        DO.Product doProduct;

        //Checking if the cart detaile are valid
        if (String.IsNullOrEmpty(myCart.CustomerName))
            throw new BO.IncorrectName("Customer name is empty");
        if (String.IsNullOrEmpty(myCart.CustomerAddress))
            throw new BO.IncorrectAddress("Customer address is empty");
        Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
        if (myCart.CustomerEmail == null || (emailRegex.IsMatch(myCart.CustomerEmail)) == false)
            throw new BO.IncorrectEmailAddress("Customer email address is empty or not in correct format");
        foreach(BO.OrderItem ? item in myCart.Items) //!!!beaya!!!
        {
            try //Checking if the productId exists 
            {
                doProduct = dal?.Product.GetById(item.ProductID)
                ?? throw new BO.InternalProblem("Dal layer is inaccessible");

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
        //finished checking the validity of the cart details

        DO.Order doOrder = new DO.Order() //Creating an order
        {
            ID = 0,
            CustomerName = myCart.CustomerName,
            CustomerEmail = myCart.CustomerEmail,
            CustomerAddress = myCart.CustomerAddress,
            OrderDate = DateTime.Now, //Date for starting the order
            ShipDate = null,
            DeliveryDate = null
        };
        int orderID;
        try
        {
            orderID = dal.Order.Add(doOrder); //Trying to add the order to the Dal layer
        }
        catch
        {
            throw new BO.InternalProblem("Dal layer is inaccessible");
        }
        foreach (BO.OrderItem? item in myCart.Items) //Creating order items based on the items in the cart
        {
            DO.OrderItem doOrderItem = new DO.OrderItem()
            {
                ID = item.ID,
                OrderID = orderID,
                ProductID = item.ProductID, 
                Price = (double)item.Price,
                Amount = item.Amount
            };
            try
            {
                dal?.OrderItem.Add(doOrderItem); //trying to add the order item
            }
            catch
            {
                throw new BO.InternalProblem("Dal layer is inaccessible");
            }
        }
        DO.Product myProduct;
        foreach (BO.OrderItem? item in myCart.Items) //Going through the items in the cart
        {
            try //Checking if the productId exists 
            {
                myProduct = dal.Product.GetById(item.ProductID);
            }
            catch (DO.NotExists ex)
            {
                throw new BO.EntityNotExist("Product ID is not valid", ex);
            }
            myProduct.InStock -= item.Amount; //Updating the amount of the product in stock while the order is being confirmed 
        }
    }
}
