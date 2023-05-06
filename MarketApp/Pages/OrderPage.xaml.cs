using MarketApp.Notication;
using MarketCore.API.MarketAPI;
using Pages;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
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
            Update_Orders();

            aTimer = new Timer(45000);
            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.Enabled = true;
        }

        private void ATimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Update_Orders();
        }

        private void Add_order_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var page = new WindowForm();
            page.ShowDialog();
            Update_Orders();
        }

        private async void Remove_order_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var requst = await MarketAPI.Instance.SetOrderAsync(Selected_Order.Hash_name, "", "0", "0");

            if (requst.Success)
                Notification.DisplayInfo("Order deleted");

            if (requst.Success == false)
                Notification.DisplayInfo("Failed to complete action");

            Update_Orders();
        }

        private void Update_order_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var page = new WindowForm(Selected_Order);
            page.ShowDialog();
            Update_Orders();
        }

        private void Active_Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Remove_order.IsEnabled = true;
            Update_order.IsEnabled = true;

            if (e.AddedItems.Count >= 1)
            {
                if (e.AddedItems[0] is Order selectedOrder)
                {
                    Selected_Order = selectedOrder;
                }
            }

        }
        private void Update_Orders()
        {
            Task.Run(() =>
            this.Dispatcher.Invoke(new Action(async () =>
            {
                var orders_ = await MarketAPI.Instance.GetOrdersAsync();
                Active_Orders.ItemsSource = orders_.Orders;

                var orderslog = await MarketAPI.Instance.GetOrdersLogAsync();

                if (orderslog.Orders != null && orderslog.Orders.Count != 0)
                    History_Orders.ItemsSource = orderslog.Orders;

                Spinner1.Visibility = System.Windows.Visibility.Collapsed;
                Orders.Visibility = System.Windows.Visibility.Visible;
            })));
        }
    }
}
