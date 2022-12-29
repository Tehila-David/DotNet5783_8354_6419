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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;
        public OrderWindow(int id)
        {
            InitializeComponent();
            OrderDetails.Text = bl.Order.GetByID(id).ToString();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
