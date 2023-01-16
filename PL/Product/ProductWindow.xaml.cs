using BlApi;
using BO;
using PL.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace PL
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;

      

        public static readonly DependencyProperty ProductDependency = DependencyProperty.Register(nameof(Product), typeof(BO.Product), typeof(Window));
        public BO.Product? Product { get => (BO.Product)GetValue(ProductDependency); private set => SetValue(ProductDependency, value); }
        public Array CategoryArray { get { return Enum.GetValues(typeof(BO.Category)); } }

        Action<BO.ProductForList> ActionProduct;//Add product to Products-ObservableCollection
        /// <summary>
        /// Constructor of ProductWindow
        /// </summary>
        /// <param name="ActProduct"></param>
        /// <param name="id"></param>
        public ProductWindow( Action<BO.ProductForList> ActProduct, int id = 0)
        {
            try
            {
                Product = id == 0 ? new() { Category = BO.Category.NO_ONE } : bl.Product.GetById(id);
                
                InitializeComponent();
                if (id == 0)
                {
                    Update.Visibility=Visibility.Hidden;
                }
                else
                {
                    Add.Visibility=Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                Close();
                MessageBox.Show(ex.Message, "Failure getting entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            ActionProduct = ActProduct;
        }
       
        /// <summary>
        /// click on button ADD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.Add(Product);
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Check your input and try again");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            BO.ProductForList productForList = new ProductForList
            {
                Price = Product.Price,
                ID = Product.ID,
                Category = Product.Category,
                Name = Product.Name,
               
            };
            ActionProduct(productForList);
           
        }

        
        /// <summary>
        /// click on button UPDATE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                bl.Product.Update(Product);
                Close ();   
            }
            catch (FormatException)
            {
                MessageBox.Show("Check your input and try again");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            BO.ProductForList productForList = new ProductForList
            {
                Price = Product.Price,
                ID = Product.ID,
                Category = Product.Category,
                Name = Product.Name,
            };
            ActionProduct(productForList);
        }
        //private void Remove_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        bl.Product.Delete(Product.ID);
        //        Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

    }
}
