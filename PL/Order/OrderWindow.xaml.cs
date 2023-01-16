using System;
using System.Windows;
using System.Windows.Controls;

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
            Order = bl.Order.GetByID(id);


        }

        /// <summary>
        /// constructor for Client
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        public OrderWindow(int id, bool flag)
        {
            InitializeComponent();
            Order = bl.Order.GetByID(id);


        }

        //private void Update_Items_Click(object sender, RoutedEventArgs e)
        //{
        //    bl.Order.UpdateShipDate(Order.ID);
        //    bl.Order.UpdateDelivery(Order.ID);
        //    new OrderItemWindow(Order.ID).Show();
        //}

        private void Update_DeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order = bl.Order.UpdateDelivery(Order.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }



        private void Update_ShipDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order = bl.Order.UpdateShipDate(Order.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Update item in Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Items_List(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            BO.OrderItem orderItem = new BO.OrderItem();
            orderItem = listBox.SelectedItem as BO.OrderItem;

            new OrderItemWindow(Order.ID,orderItem.ID).Show();
            
        }

        
    }
}
