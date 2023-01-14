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
            var items = bl.Product.GetListProductItem(myLovelyCart);
            Products = items == null ? new() : new(items);
            InitializeComponent();
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
       /// click on butten Add product to cart 
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                BO.ProductItem productItem = (BO.ProductItem)((sender as Button)!.DataContext!);
                if (productItem.IsAvailable == true)
                {
                    bl.Cart.AddProduct(myLovelyCart, productItem.ID);
                    var temp = bl.Product.GetListProductItem(myLovelyCart);
                    Products = temp == null ? new() : new(temp);
                }
                else
                    throw new Exception("We sorry this product out of stock");
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
        /// click on butten Remove product from cart 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.ProductItem productItem = (BO.ProductItem)((sender as Button)!.DataContext!);
                bl.Cart.UpdateProductAmount(myLovelyCart, productItem.ID, productItem.Amount - 1);
                var temp = bl.Product.GetListProductItem(myLovelyCart);
                Products = temp == null ? new() : new(temp);
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
        /// Filter - list productItems by category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CatagorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = myCategory == BO.Category.NO_ONE ?
            bl.Product.GetListProductItem(myLovelyCart) : bl.Product.GetListProductItem(myLovelyCart).Where(item => item.Category == myCategory);
            Products = temp == null ? new() : new(temp);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
