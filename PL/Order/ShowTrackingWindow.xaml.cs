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
    /// Interaction logic for ShowTrackingWindow.xaml
    /// </summary>
    public partial class ShowTrackingWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;

       public static readonly DependencyProperty TrackingDependency =
       DependencyProperty.Register(nameof(tracking),
                             typeof(BO.OrderTracking),
                             typeof(ShowTrackingWindow));
        public BO.OrderTracking? tracking
        {
            get => (BO.OrderTracking)GetValue(TrackingDependency);
            private set => SetValue(TrackingDependency, value);
        }

        /// <summary>
        /// Constructor for  displaying orderTracking
        /// </summary>
        /// <param name="Id"></param>
        public ShowTrackingWindow(int Id)
        {
            InitializeComponent();
            tracking=bl.Order.followOrder(Id);

        }
    }
}
