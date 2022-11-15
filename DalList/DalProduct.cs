
using DO;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

public class DalProduct
{
    public int addProduct(Product myProduct)
    {
        
        if (DataSource.arrayProducts.Length == DataSource.Config.IndexProducts)
        {
            throw new Exception("The product array is full");
        }
        for (int i = 0; i < DataSource.Config.IndexProducts; i++)
        {
              if (DataSource.arrayProducts[i].ID == myProduct.ID)
            {
                throw new Exception("The product already exists");
            }
        }
        DataSource.arrayProducts[DataSource.Config.IndexProducts] = new Product()
        { ID = myProduct.ID,
            Name = myProduct.Name,
            Price = myProduct.Price,
            Category = myProduct.Category,
            InStock = myProduct.InStock };
        DataSource.Config.IndexProducts++;
        return myProduct.ID;
    }

    public Product getSingleProduct(int id)
    {
        for (int i = 0; i < DataSource.Config.IndexProducts; i++)
        {
            if (id == DataSource.arrayProducts[i].ID)
            {
                Product singleProduct = new Product()
                {
                    ID = DataSource.arrayProducts[i].ID,
                    Name = DataSource.arrayProducts[i].Name,
                    Price = DataSource.arrayProducts[i].Price,
                    Category = DataSource.arrayProducts[i].Category,
                    InStock = DataSource.arrayProducts[i].InStock
                };
                return singleProduct;
            }
        }
        throw new Exception("Sorry ,this product does not exist in the array ");
    }
  //   public  IEnumerable<Product> getListOfProducts()
     public  Product[] getListOfProducts()
    {    
         return  Array.FindAll(DataSource.arrayProducts, p => p.ID != 0);
        //return  DataSource.arrayProducts.Where(p => p.ID != 0);
    }
    public void deleteProduct(int ID)
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
        throw new Exception("Sorry ,this product does not exist in the array ");

    }

    public void updateProduct(Product myProduct)
    {
        for (int i = 0; i < DataSource.Config.IndexProducts; i++)
        {
            if (DataSource.arrayProducts[i].ID == myProduct.ID)
            {
                DataSource.arrayProducts[i] = myProduct;
                break;
            }
        }
        throw new Exception("Product to be updated does not exist");
    }

}
