using System;
using System.Collections.Generic;
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
    public partial class AddProductWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;

        int IdForUpdate = 0;

        /// <summary>
        ///constructor of Add product
        /// </summary>
        public AddProductWindow()
        {
            InitializeComponent();

            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            Update.Visibility = Visibility.Hidden;
        }

        /// <summary> 
        /// constructor of Update product
        /// </summary>
        /// <param name="product"></param>
        public AddProductWindow(int ID)
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            Add.Visibility = Visibility.Hidden;
            LabelID.Visibility= Visibility.Hidden;
            txtID.Visibility= Visibility.Hidden;    
            IdForUpdate = ID;
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
                BO.Product product = new BO.Product();
                product.ID = int.Parse(txtID.Text);
                product.Name = txtName.Text;
                product.InStock = int.Parse(txtInStock.Text);
                product.Price = int.Parse(txtPrice.Text);
                product.Category = (BO.Category)CategorySelector.SelectedItem;
                bl.Product.Add(product);
               
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
                BO.Product product = new BO.Product();
                product.ID = IdForUpdate;
                product.Name = txtName.Text;
                product.InStock = int.Parse(txtInStock.Text);
                product.Price = int.Parse(txtPrice.Text);
                product.Category = (BO.Category)CategorySelector.SelectedItem;
                bl.Product.Update(product);
                new ProductListWindow().Show();
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

        

    }
}
