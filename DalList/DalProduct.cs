
using DO;

namespace Dal;

public class DalProduct
{
    /// This function adds an order item
    public int addProduct(Product myProduct)
    {
        /// Checking if the length of the array is equal to the index of the last occupied space
        if (DataSource.arrayProducts.Length == DataSource.Config.IndexProducts)
        {
            throw new Exception("The product array is full");
        }
        ///Going through the array
        for (int i = 0; i < DataSource.Config.IndexProducts; i++)
        {
            ///Checking if the id of the requested product is found in the array of products
            if (DataSource.arrayProducts[i].ID == myProduct.ID)
            {
                throw new Exception("The product already exists");
            }
        }
        ///Creating the new product to be added
        DataSource.arrayProducts[DataSource.Config.IndexProducts] = new Product()
        { ID = myProduct.ID,
            Name = myProduct.Name,
            Price = myProduct.Price,
            Category = myProduct.Category,
            InStock = myProduct.InStock };
        DataSource.Config.IndexProducts++; ///Increasing the amount of occupied products in the array
        return myProduct.ID;
    }

    /// This function returns a product based on the useer input id
    public Product getSingleProduct(int id)
    {
        ///Going through the array of products
       
        for (int i = 0; i < DataSource.Config.IndexProducts; i++)
        {
            ///Checking if the id the user entered is equal to an id in the array 
            if (id == DataSource.arrayProducts[i].ID)
            {
                ///Creating a new product with the details of the product with the id the user entered
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
        /// If the id the user entered is not found in the array
        throw new Exception("Sorry ,this product does not exist in the array ");
    }
    /// This function returns all of the products
     public  Product[] getListOfProducts()
    {
        ///looking for all of the products that have their details filed in and returning them
        return Array.FindAll(DataSource.arrayProducts, p => p.ID != 0);
    }
    /// This function deletes a product from the array of products
    public void deleteProduct(int ID)
    {
        //int nextIndex = DataSource.Config.IndexProducts; //The amount of occupirs places in the array
        ///Going tthrough the array
        for (int i = 0; i < DataSource.Config.IndexProducts; i++)
        {
            // Checking if th id in the array is equal to the id the user entered
            if (DataSource.arrayProducts[i].ID == ID)
            {
                /// Going through the array from  the point of the found id
                for (int j = i; j < DataSource.Config.IndexProducts - 1; j++)
                {
                    ///moving each product one space to the left
                    DataSource.arrayProducts[j] = DataSource.arrayProducts[j + 1];
                }
                DataSource.Config.IndexProducts--; //Decreasing the amount of products by one
                break;
            }
        }
        // If the id the user entered was not found inthe array
        throw new Exception("Sorry ,this product does not exist in the array ");

    }

    //This function receives a product and updates an existing product with it
    public void updateProduct(Product myProduct)
    {
        // Going through the product array
        for (int i = 0; i < DataSource.Config.IndexProducts; i++)
        {
            // Checking if th id in the array is equal to the id  of the product the user entered
            if (DataSource.arrayProducts[i].ID == myProduct.ID)
            {
                //updating the product
                DataSource.arrayProducts[i] = myProduct;
                break;
            }
        }
        //If the id of the product the user entered to be updated is not found in the array
        throw new Exception("Product to be updated does not exist");
    }

}
