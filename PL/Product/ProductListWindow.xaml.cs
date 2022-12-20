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
           //CategorySelector.ItemsSource = /*bl.Product(CategorySelector.SelectedItem);*/
        }

        private void ProductsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CategorySelector.SelectedItem = Enum.GetValues(typeof(BO.Category));
            ProductsList.ItemsSource = bl.Product.GetListedProducts(product=> product?.Category ?? throw new NullReferenceException("Missing product category") == (BO.Category)CategorySelector.SelectedItem);
        }
    }
}
