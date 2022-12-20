using BlApi;
using BlImplementation;
using DO;
using System;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl = new Bl();
        public ProductListWindow()
        {
           InitializeComponent();
           ProductsList.ItemsSource=bl.Product.GetListedProducts();
           CategorySelector.ItemsSource= Enum.GetValues(typeof(BO.Category));
           CategorySelector.SelectedItem= Enum.GetValues(typeof(BO.Category));

            Func<DO.Product?, bool>? predicate = item =>
            {
                bool b1 = item?.Category == (DO.Category)CategorySelector.SelectedItem;
                return b1;
            };
            CategorySelector.ItemsSource = bl.Product.GetListedProducts(predicate);
        }

        private void ProductsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CategorySelector.SelectedItem = Enum.GetValues(typeof(BO.Category));
            //ProductsList.ItemsSource = bl.Product.GetListedProducts(product=> product?.Category ?? throw new NullReferenceException("Missing product category") == (BO.Category)CategorySelector.SelectedItem);
        }
    }
}
