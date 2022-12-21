using BlApi;
using BlImplementation;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private void MainWindow_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();

        public MainWindow()
        {
            InitializeComponent();
        }
        private IBl bl = new Bl();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
             new ProductListWindow().Show();
        }
    }
}
