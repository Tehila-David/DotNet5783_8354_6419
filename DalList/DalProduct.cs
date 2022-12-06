
using DO;
using DalApi;

namespace Dal;

internal class DalProduct : IProduct
{
    DataSource _dataSource = DataSource.s_instance;

    /// <summary>
    /// This function adds an Product
    /// </summary>
    public int Add(Product myProduct)
    {
        if(_dataSource.ProductList.Exists(p => p.ID== myProduct.ID))
             throw new AlreadyExists("The product already exists");
        _dataSource.ProductList.Add(myProduct);
        return myProduct.ID;
    }

    /// <summary>
    /// This function returns a product based on the useer input id
    /// </summary>
    public Product GetById(int id)
    {
        foreach (var item in _dataSource.ProductList)
        {   ///Checking if the id the user entered is equal to an id in the list
            if (id == item.ID)
            {
                return item;
            }
        }
        /// If the id the user entered is not found in the list
        throw new NotExists("Sorry ,this product does not exist in the List ");
    }

    /// <summary>
    /// This function returns all of the products
    /// </summary>
    public IEnumerable<Product> GetAll()
    {
        ///looking for all of the products that have their details filed in and returning them
        return _dataSource.ProductList.ToList();
    }

    /// <summary>
    /// This function deletes a product from the list of products
    /// </summary>
    public void Delete(int ID)
    {

        foreach (var item in _dataSource.ProductList)
        {  // Checking if th id in the list is equal to the id the user entered
            if (item.ID == ID)
            {

                _dataSource.ProductList.Remove(item);
                return;

            }
        }
        // If the id the user entered was not found in the list 
        throw new NotExists("Sorry ,this product does not exist in the list ");

    }

    /// <summary>
    /// This function receives a product and updates an existing product with it
    /// </summary>
    public void Update(Product myProduct)
    {
        int index = 0;
        foreach (var item in _dataSource.ProductList)
        {
            if (item.ID == myProduct.ID) ///updating the order
            {
                _dataSource.ProductList.RemoveAt(index);
                _dataSource.ProductList.Insert(index, myProduct);
                return;
            }
            index++;
        }
        //If the id of the product the user entered to be updated is not found in the list
        throw new NotExists("Product to be updated does not exist");
    }
}
