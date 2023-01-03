
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;
        public ProductListWindow()
        {
           InitializeComponent();
           ProductsList.ItemsSource=bl.Product.GetListedProducts();
           CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));   
        }

        /// <summary>
        /// Filter - list products by category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            BO.Category category = (BO.Category)CategorySelector.SelectedItem;
            Func<DO.Product?, bool>? predicate = item =>
            {
                bool b1 = item?.Category == (DO.Category)category;
                return b1;
            };

            ProductsList.ItemsSource = bl.Product.GetListedProducts(predicate);
        }

        /// <summary>
        /// Click Button of adding products 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            new AddProductWindow().Show();
            Close();
        }

        /// <summary>
        /// click on product from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox listBox= sender as ListBox;
            BO.ProductForList product = new BO.ProductForList();
            product = listBox.SelectedItem as BO.ProductForList;
            
            new AddProductWindow(product.ID).Show();
            Close();
        }
       

    }

    //class listProducts : DependencyObject
    //{
    //    public IEnumerable<Product?> ProductsList
    //    {
    //        get { return (IEnumerable<Product?>)GetValue(MyPropertyProperty); }
    //        set { SetValue(MyPropertyProperty, value); }
    //    }

    //    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    //    public static readonly DependencyProperty MyPropertyProperty =
    //        DependencyProperty.Register("ProductsList", typeof(int), typeof(ProductListWindow), new PropertyMetadata(0));

    //}


}
