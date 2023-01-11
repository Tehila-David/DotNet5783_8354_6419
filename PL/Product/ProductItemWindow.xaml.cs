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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;

      
        public static readonly DependencyProperty ProductItemDependency =
        DependencyProperty.Register(nameof(ProductItem),
                               typeof(BO.ProductItem),
                               typeof(ProductItemWindow));
        public BO.Product? ProductItem
        {
            get => (BO.Product)GetValue(ProductItemDependency);
            private set => SetValue(ProductItemDependency, value);
        }
        public ProductItemWindow(int productID)
        {
            //InitializeComponent();
           
        }

        private void AddCart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveCart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
