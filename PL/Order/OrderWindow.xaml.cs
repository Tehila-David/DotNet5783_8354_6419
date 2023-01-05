using DO;
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
using System.Xml.Linq;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
   
        BlApi.IBl bl = BlApi.Factory.Get()!;

        public static readonly DependencyProperty OrderDependency =
       DependencyProperty.Register(nameof(Order),
                              typeof(BO.Order),
                              typeof(OrderWindow));
        public BO.Order? Order
        {
            get => (BO.Order)GetValue(OrderDependency);
            private set => SetValue(OrderDependency, value);
        }


        public BO.OrderStatus Status { get; set; } 

        public Array StatusArray { get { return Enum.GetValues(typeof(BO.OrderStatus)); } }

        /// <summary>
        /// constructor for Manager
        /// </summary>
        /// <param name="id"></param>
        public OrderWindow(int id)
        {
            InitializeComponent();
            Order=bl.Order.GetByID(id);
            

        }

        /// <summary>
        /// constructor for Client
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        public OrderWindow(int id,bool flag)
        {
            InitializeComponent();
            Order = bl.Order.GetByID(id);
            Update_Items.Visibility = Visibility.Hidden;

        }

        private void Update_Items_Click(object sender, RoutedEventArgs e)
        {
            bl.Order.UpdateShipDate(Order.ID);
            bl.Order.UpdateDelivery(Order.ID);
            new OrderItemWindow(Order.ID).Show();
        }

        private void Update_Order_Details_Click(object sender, RoutedEventArgs e)
        {
            bl.Order.UpdateShipDate(Order.ID);
            bl.Order.UpdateDelivery(Order.ID);
            
        }

    }
}
