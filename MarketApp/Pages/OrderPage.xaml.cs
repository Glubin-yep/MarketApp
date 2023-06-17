using MarketApp.Notification;
using MarketCore.API.MarketAPI;
using Pages;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;
using static MarketCore.API.MarketAPI.Models.OrdersModel;

namespace MarketApp.Pages
{
    public partial class OrderPage : Page
    {
        private Order Selected_Order { get; set; }
        private readonly Timer aTimer;

        public OrderPage()
        {
            InitializeComponent();
            aTimer = new Timer(180000);
            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.Enabled = true;
        }

        private async void ATimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            await LoadOrders();
        }

        private async void Add_order_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var page = new WindowForm();
            page.ShowDialog();
            await LoadOrders();
        }

        private async void Remove_order_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var request = await MarketAPI.Instance.SetOrderAsync(Selected_Order.Hash_name, "", "0", "0");
            var message = request.Success ? "Order deleted" : "Failed to complete action";
            WindowsNotification.DisplayInfo(message);
            await LoadOrders();
        }

        private async void Update_order_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var page = new WindowForm(Selected_Order);
            page.ShowDialog();
            await LoadOrders();
        }

        private void Active_Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var hasSelectedItems = e.AddedItems.Count >= 1;
            Remove_order.IsEnabled = hasSelectedItems;
            Update_order.IsEnabled = hasSelectedItems;

            if (e.AddedItems.Count >= 1)
            {
                if (e.AddedItems[0] is Order selectedOrder)
                {
                    Selected_Order = selectedOrder;
                }
            }
        }

        private async Task<bool> LoadOrders()
        {
            var orders = await MarketAPI.Instance.GetOrdersAsync();
            await Dispatcher.InvokeAsync(() => Active_Orders.ItemsSource = orders.Orders);
            return false;
        }

        private async Task<bool> LoadHistoryOrders()
        {
            var ordersLog = await MarketAPI.Instance.GetOrdersLogAsync();

            if (ordersLog.Orders != null && ordersLog.Orders.Count != 0)
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    History_Orders.ItemsSource = ordersLog.Orders;
                    History_Orders.Visibility = System.Windows.Visibility.Visible;
                });
                return true;
            }

            return false;
        }

        private async void Load_orders_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Spinner1.Visibility = System.Windows.Visibility.Visible;
            Active_Orders.Visibility = System.Windows.Visibility.Collapsed;

            await LoadOrders();
            await LoadHistoryOrders();

            Spinner1.Visibility = System.Windows.Visibility.Collapsed;
            Active_Orders.Visibility = System.Windows.Visibility.Visible;
        }

        private void LayoutListbox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = Active;
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
