using BO;
using PL.Cart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class CatalogWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;

        public static readonly DependencyProperty ProductsDependency =
                DependencyProperty.Register(nameof(Products),
                                        typeof(ObservableCollection<ProductItem?>),
                                        typeof(CatalogWindow));
        public ObservableCollection<ProductItem?> Products
        {
            get => (ObservableCollection<ProductItem?>)GetValue(ProductsDependency);
            private set => SetValue(ProductsDependency, value);
        }


       public BO.Category myCategory { get; set; } = BO.Category.NO_ONE;

        public Array CategoryArray { get { return Enum.GetValues(typeof(BO.Category)); } }
        BO.Cart myLovelyCart;
        public CatalogWindow(BO.Cart myCart)
        {
            myLovelyCart = myCart;
            var items = bl.Product.GetListProductItem();
            Products = items == null ? new() : new(items);
            InitializeComponent();
            DataContext = Products;
        }



        /// <summary>
        /// Click Button of Cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowCart_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new CartWindow(myLovelyCart).Show();
        }

        /// <summary>
        /// click on product from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductsItemList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            BO.ProductItem product = new BO.ProductItem();
            product = listBox.SelectedItem as BO.ProductItem;
            //new ProductWindow(product.ID,true, myLovelyCart).Show();
            
        }

        /// <summary>
        /// Filter - list products by category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = myCategory == BO.Category.NO_ONE ?
           bl.Product.GetListProductItem() : bl.Product.GetListProductItem().Where(item => item.Category == myCategory);
            Products = temp == null ? new() : new(temp);
        }

        private void ProductItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                BO.ProductItem productItem = (BO.ProductItem)((sender as Button)!.DataContext!);
                bl.Cart.AddProduct(myLovelyCart, productItem.ID);
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

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.ProductItem productItem = (BO.ProductItem)((sender as Button)!.DataContext!);
                bl.Cart.UpdateProductAmount(myLovelyCart, productItem.ID, 0);
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
