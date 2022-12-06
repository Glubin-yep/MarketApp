using MarketBot.API;
using Pages;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using static MarketApp.Date.OrdersDate;

namespace MarketBot.Pages
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
            await MarketAPI.SetOrderAsync(Selected_Order.Hash_name, "", "0", "0");
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
                Selected_Order = (Order)e.AddedItems[0];
            }
        }
        private void Update_Orders()
        {
            Task.Run(() =>
            this.Dispatcher.Invoke(new Action(async () =>
            {
                var orders_ = await MarketAPI.GetOrdersAsync();
                Active_Orders.ItemsSource = orders_.Orders;

                var orderslog = await MarketAPI.GetOrdersLogAsync();

                if (orderslog.Orders.Count > 0)
                    History_Orders.ItemsSource = orderslog.Orders;
            })));
        }
    }
}
