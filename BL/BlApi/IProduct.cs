using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;

namespace BlApi;

public interface IProduct
{
    public int Add(Product myProduct);
    public Product GetById(int id);
    public IEnumerable<Product> GetAll();
    public void Delete(int ID);
    public void Update(Product myProduct);

}
