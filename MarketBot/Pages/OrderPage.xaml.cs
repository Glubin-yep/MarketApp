using MarketBot.API;
using Pages;
using System;
using System.Timers;
using System.Windows.Controls;

namespace MarketBot.Pages
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public string? Selected_Order_Name { get; private set; }
        public object? Selected_Order { get; private set; }
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
            var page = new WindowForm(null);
            page.ShowDialog();
        }

        private void Remove_order_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MarketAPI.SetOrder(Selected_Order_Name, "", "0", "0");
        }

        private void Update_order_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var page = new WindowForm(Selected_Order);
            page.ShowDialog();
        }

        private void Active_Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Remove_order.IsEnabled = true;
            Update_order.IsEnabled = true;

            if (e.AddedItems.Count >= 1)
            {
                Selected_Order = e.AddedItems[0];
                var nameOfProperty = "hash_name";
                var propertyInfo = e.AddedItems[0].GetType().GetProperty(nameOfProperty);
                Selected_Order_Name = propertyInfo.GetValue(e.AddedItems[0], null).ToString();
            }
        }
        private void Update_Orders()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                var orders_ = MarketAPI.GetOrders();
                Active_Orders.ItemsSource = orders_.orders;
                var orderslog = MarketAPI.GetOrdersLog();

                if (orderslog.orders.Count > 0)
                    History_Orders.ItemsSource = orderslog.orders;
            }));
            
        }
    }
}
