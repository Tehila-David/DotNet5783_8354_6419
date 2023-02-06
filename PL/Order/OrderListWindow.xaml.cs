using BO;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;


        public static readonly DependencyProperty OrdersDependency =
       DependencyProperty.Register(nameof(Orders),
                               typeof(ObservableCollection<OrderForList?>),
                               typeof(OrderListWindow));
        public ObservableCollection<OrderForList?> Orders
        {
            get => (ObservableCollection<OrderForList?>)GetValue(OrdersDependency);
            private set => SetValue(OrdersDependency, value);
        }
        

        public BO.OrderStatus Status { get; set; }= OrderStatus.Default;
        public Array StatusArray { get { return Enum.GetValues(typeof(BO.OrderStatus)); } }


        /// <summary>
        /// constructor for Orders List
        /// </summary>
        public OrderListWindow()
        {
            InitializeComponent();
            var item = bl.Order.GetListedOrders();
            Orders = item == null ? new() : new(item);
        }


        private void OrdersList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)// click on Order from list
        {
            ListBox listBox = sender as ListBox;
            BO.OrderForList order = new BO.OrderForList();
            order = listBox.SelectedItem as BO.OrderForList;
            new OrderWindow(order.ID).Show();
            Close();
        }

      
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)//Sort by status
        {
            var temp = Status == BO.OrderStatus.Default ?
           bl.Order.GetListedOrders() : bl.Order.GetListedOrders().Where(item => item.Status == Status);
            Orders = temp == null ? new() : new(temp);
        }
    }
}
