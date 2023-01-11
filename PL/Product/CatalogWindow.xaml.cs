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


        public BO.Category Category { get; set; } = BO.Category.NO_ONE;

        public Array CategoryArray { get { return Enum.GetValues(typeof(BO.Category)); } }
        BO.Cart myLovelyCart;
        public CatalogWindow(BO.Cart myCart)
        {
            InitializeComponent();
            myLovelyCart = myCart;
            var item = bl.Product.GetListProductItem();
            Products = item == null ? new() : new(item);
        }

      
        
        /// <summary>
        /// Click Button of Cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
            var temp = Category == BO.Category.NO_ONE ?
           bl.Product.GetListProductItem() : bl.Product.GetListProductItem().Where(item => item.Category == Category);
            Products = temp == null ? new() : new(temp);
        }

       
    }
}
