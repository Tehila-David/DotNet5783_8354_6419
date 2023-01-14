
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;


        public static readonly DependencyProperty ProductsDependency =
        DependencyProperty.Register(nameof(Products),
                                typeof(ObservableCollection<ProductForList?>),
                                typeof(ProductListWindow));
        public  ObservableCollection<ProductForList?> Products
        {
            get => (ObservableCollection<ProductForList?>)GetValue(ProductsDependency);
            private set => SetValue(ProductsDependency, value);
        }
       
        /// <summary>
        /// for Enum Category
        /// </summary>
        public BO.Category Category { get; set; } = BO.Category.NO_ONE;

        public Array CategoryArray { get { return Enum.GetValues(typeof(BO.Category)); } }

        public ProductListWindow()
        {
           InitializeComponent();
           var item  = bl.Product.GetListedProducts();
           Products = item == null ? new() : new(item);

        }


        /// <summary>
        /// Filter - list products by category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var temp = Category == BO.Category.NO_ONE ?
            bl.Product.GetListedProducts() : bl.Product.GetListedProducts().Where(item => item?.Category == Category);
            Products = temp == null ? new() : new(temp);

        }

        /// <summary>
        /// Click Button of adding products 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                new ProductWindow(addProduct).Show(); 
            }
            catch (FormatException )
            {
                MessageBox.Show("Check your input and try again");
            }
            var item = bl.Product.GetListedProducts();
            Products = item == null ? new() : new(item);

        }

        /// <summary>
        /// click on product from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try 
            {
                ListBox listBox = sender as ListBox;
                BO.ProductForList? product = new BO.ProductForList();
                product = listBox.SelectedItem as BO.ProductForList;
                new ProductWindow(addProduct, product.ID).Show();
                Products.Remove(product);
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
        public void addProduct(BO.ProductForList product)=> Products?.Add(product);//Add product to Products-ObservableCollection




    }



    


}
