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
        }
        
        private void ProductsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }

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
    }
}
