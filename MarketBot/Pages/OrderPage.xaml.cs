using MarketBot.API;
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
        }
    }
}
