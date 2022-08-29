using MarketBot.API;
using Pages;
using System.Windows.Controls;

namespace MarketBot.Pages
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            var orders_ = MarketAPI.GetOrders();
            Active_Orders.ItemsSource = orders_.orders;
            var orderslog = MarketAPI.GetOrdersLog();

            if(orderslog.orders.Count > 0)
                History_Orders.ItemsSource = orderslog.orders;
        }

        private void Add_order_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var page = new WindowForm();
            page.ShowDialog();
        }

        private void Remove_order_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Update_order_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
