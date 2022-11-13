
using DO;
using System.Diagnostics;

namespace Dal;

public class DalProduct
{
    public int addProduct(Product myProduct)
    {
        int nextIndex = DataSource.Config.IndexProducts;
        if (DataSource.arrayProducts.Length == nextIndex)
        {
            throw new Exception("The order item array is full");
        }
       for(int i = 0; i < nextIndex; i++)
        {
           if( DataSource.arrayProducts[i].ID == myProduct.ID)
            {
                throw new Exception("The order item already exists");
            }

        }
        DataSource.arrayProducts[nextIndex] = new Product()
        {ID = add.ID, 
         Name = add.Name, 
         Price = add.Price, 
         Category = add.Category, 
         InStock = add.InStock};

        return add.ID;
    }
    public void delete(int ID)
    {
        int nextIndex = DataSource.Config.IndexProducts;
        for (int i = 0; i < nextIndex; i++)
        {
            if (DataSource.arrayProducts[i].ID == ID)
            {
                for (int j = i; j < nextIndex - 1; j++)
                {
                    DataSource.arrayProducts[j] = DataSource.arrayProducts[j + 1];
                }
                DataSource.Config.IndexProducts--;
                break;
            }
        }
        throw new Exception("Sorry ,this is not exist in the array ");

    }

}
