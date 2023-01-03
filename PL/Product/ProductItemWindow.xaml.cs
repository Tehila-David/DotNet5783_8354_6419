using System;
using System.Collections.Generic;
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
    /// List of products in the catalog
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;
        public ProductItemWindow()
        {
            InitializeComponent();
            ProductsItemList.ItemsSource = bl.Product.GetListedProducts();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void ProductsItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category category = (BO.Category)CategorySelector.SelectedItem;
            Func<DO.Product?, bool>? predicate = item =>
            {
                bool b1 = item?.Category == (DO.Category)category;
                return b1;
            };

            ProductsItemList.ItemsSource = bl.Product.GetListedProducts(predicate);
        }
        
        /// <summary>
        /// Click Button of adding products 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            
        }

        /// <summary>
        /// click on product from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductsItemList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            BO.ProductForList product = new BO.ProductForList();
            product = listBox.SelectedItem as BO.ProductForList;

            
           
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
