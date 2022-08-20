using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace MarketBot
{
    public partial class MainWindow : Window
    {
        private readonly Timer aTimer;
        public static string current_item = string.Empty;
        public static string current_sell_item = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            LoadUserInfo();
            LoadNotifyIcon();
            HttpGetInfo.ReadConfig();

            aTimer = new Timer(60000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }
        private void LoadNotifyIcon()
        {
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("MarketApp.ico");
            ni.Visible = true;
            ni.DoubleClick += Ni_DoubleClick;
        }
        private void LoadUserInfo()
        {
            UpdateStatus();
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Money.Content = HttpGetInfo.GetMoney();
                    Photo.Source = HttpGetInfo.GetAvatar();
                    Nickname.Content = HttpGetInfo.GetNickname();
                }));
            });

        }
        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    bool status = HttpGetInfo.GetPing();
                    if (status == true)
                        TradeStatus.Content = "Connected :)";
                }));
            });
        }

        private void Iteams_Button_Click(object sender, RoutedEventArgs e)
        {
            ListUpdate(1);
        }

        private void Inventory_Button_Click(object sender, RoutedEventArgs e)
        {
            ListUpdate(0);
        }

        public void ListUpdate(int mode) // 0 == inventory // 1 == Items
        {
            if (mode == 0)
            {
                InventoryLB.Items.Clear();
                var items = HttpGetInfo.GetSteamInventory();
                for (int i = 0; i <= items.items.Count - 1; i++)
                {
                    InventoryLB.Items.Add(items.items[i].market_hash_name
                        + " / " + items.items[i].id);
                }
            }
            else
            {
                ItemsLB.Items.Clear();
                var items = HttpGetInfo.GetItems();
                for (int i = 0; i <= items.items.Count - 1; i++)
                {
                    ItemsLB.Items.Add(items.items[i].market_hash_name + " / "
                        + items.items[i].price + " "
                        + items.items[i].currency
                        + " / " + items.items[i].item_id);
                }
            }

        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            var sell = HttpGetInfo.SetSell(current_item, Sell_Price.Text, "RUB");
            MessageBox.Show(sell.success + sell.item_id);
            ListUpdate(0);
            ListUpdate(1);
            Sell.IsEnabled = false;
        }

        private void InventoryLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sell_Price.IsEnabled = true;
            Sell_Price.Text = "";
            if (e.AddedItems.Count >= 1)
            {
                current_item = e.AddedItems[0].ToString();
                var price = HttpGetInfo.GetMarketPrice(current_item);
                Item_Name.Content = price.data[0].market_hash_name;
                Min_Price.Content = "Min Price : " + price.data[0].price.Insert(price.data[0].price.Length - 2, ",");
                Item_Image.Source = HttpGetInfo.GetImage(current_item);
            }
        }

        private void ItemsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Remove.IsEnabled = true;
            Update.IsEnabled = true;
            Update_Price.IsEnabled = true;
            Update_Price.Text = "";

            if (e.AddedItems.Count >= 1)
            {
                current_sell_item = e.AddedItems[0].ToString();
                var price = HttpGetInfo.GetMarketPrice(current_sell_item);
                Item_Name.Content = price.data[0].market_hash_name;
                Min_Price.Content = "Min Price : " + price.data[0].price.Insert(price.data[0].price.Length - 2, ",");
                Item_Image.Source = HttpGetInfo.GetImage(current_sell_item);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var update = HttpGetInfo.SetPrice(current_sell_item, "0", "RUB");
            MessageBox.Show(update.success + update.error);
            ListUpdate(1);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var update = HttpGetInfo.SetPrice(current_sell_item, Update_Price.Text, "RUB");
            MessageBox.Show(update.success + update.error);
            ListUpdate(1);
            Update.IsEnabled = false;
        }


        private void Sell_Price_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            Sell.IsEnabled = true;
        }

        private void Update_Price_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            Update.IsEnabled = true;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();
        }
        private void Ni_DoubleClick(object? sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

    }
}
