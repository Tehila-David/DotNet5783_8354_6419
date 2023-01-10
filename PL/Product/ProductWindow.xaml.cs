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

        int IdForUpdate = 0;

        public static readonly DependencyProperty ProductDependency =
        DependencyProperty.Register(nameof(Product),
                               typeof(BO.Product),
                               typeof(ProductWindow));
        public BO.Product? Product
        {
            get => (BO.Product)GetValue(ProductDependency);
            private set => SetValue(ProductDependency, value);
        }

        public Array CategoryArray { get { return Enum.GetValues(typeof(BO.Category)); } }


        /// <summary>
        ///constructor of Add product
        /// </summary>
        public ProductWindow()
        {
            InitializeComponent();
            Update.Visibility = Visibility.Hidden;
            RemoveFromCart.Visibility = Visibility.Hidden;
            AddToCart.Visibility = Visibility.Hidden;
        }

        /// <summary> 
        /// constructor of Update product
        /// </summary>
        /// <param name="product"></param>
        public ProductWindow(int ID)
        {
            InitializeComponent();
             
            Add.Visibility = Visibility.Hidden;
            RemoveFromCart.Visibility = Visibility.Hidden;
            AddToCart.Visibility = Visibility.Hidden;
            Product = bl.Product.GetById(ID);
            IdForUpdate = ID;
        }
        /// <summary>
        /// constructor for Client
        /// </summary>
        /// <param name="ID"></param>
        /// 

        BO.Cart myCart;

        public ProductWindow(int ID,bool flag, BO.Cart myLovelyCart)
        {
            InitializeComponent();
            Add.Visibility = Visibility.Hidden;
            Update.Visibility = Visibility.Hidden;
            Product = bl.Product.GetById(ID);
            IdForUpdate = ID;
            myCart = myLovelyCart;
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
                new ProductListWindow().Show();
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

        }


        private void AddCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.AddProduct(myCart, Product.ID);
                //new ProductListWindow().Show();
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
                //new ProductListWindow().Show();
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

        }

        private void RemoveCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.UpdateProductAmount(myCart, Product.ID, 0);
                new ProductListWindow().Show();
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

        }


    }
}
