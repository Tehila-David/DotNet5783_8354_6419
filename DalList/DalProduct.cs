
using DO;

namespace Dal;

public class DalProduct
{
    public int addProduct(Product add)
    {
        int nextIndex = DataSource.Config.IndexProducts;
        if (DataSource.arrayProducts.Length == nextIndex)
        {
            throw new Exception("Sorry ,there is no place in array ");
        }
       for(int i = 0; i < nextIndex; i++)
        {
           if( DataSource.arrayProducts[i].ID == add.ID)
            {
                throw new Exception("This is exist in the array");
            }

        }
        DataSource.arrayProducts[nextIndex]=add;
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
