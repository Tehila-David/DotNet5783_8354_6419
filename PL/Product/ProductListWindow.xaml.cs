
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static readonly DependencyProperty ProductsDependency =
        DependencyProperty.Register(nameof(Products),
                                typeof(ObservableCollection<ProductForList?>),
                                typeof(ProductListWindow));
        public ObservableCollection<ProductForList?> Products
        {
            get => (ObservableCollection<ProductForList?>)GetValue(ProductsDependency);
            private set => SetValue(ProductsDependency, value);
        }

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
            bl.Product.GetListedProducts() : bl.Product.GetListedProducts().Where(item => item.Category == Category);
            Products = temp == null ? new() : new(temp);

        }

        /// <summary>
        /// Click Button of adding products 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            new ProductWindow().Show();
            Close();
        }

        /// <summary>
        /// click on product from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            BO.ProductForList product = new BO.ProductForList();
            product = listBox.SelectedItem as BO.ProductForList;

            new ProductWindow(product.ID).Show();
            Close();
        }
       

    }



    


}
