using AdonisUI.Controls;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using Telegram.Bot;

namespace MarketBot
{
    public partial class MainWindow : AdonisWindow
    {
        private readonly Timer aTimer;
        public static string? current_item;
        public static string? current_sell_item;

        public MainWindow()
        {
            InitializeComponent();
            HttpGetInfo.ReadConfig();
            LoadUserInfo();
            LoadUserHistory();

            aTimer = new Timer(45000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;

        }

       
        private static void Notification(string text)
        {
            var ni = new System.Windows.Forms.NotifyIcon
            {
                Icon = new System.Drawing.Icon("MarketApp.ico"),
                Visible = true,
                BalloonTipTitle = "Market App",
                BalloonTipText = text
            };
            ni.ShowBalloonTip(8000);
            Task.Delay(8000).Wait();
            ni.Dispose();
        }

        private void LoadUserInfo()
        {
            UpdateStatus();
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Money.Content = HttpGetInfo.GetMoney() + " " + HttpGetInfo.Market_currency;
                    Photo.Source = HttpGetInfo.GetAvatar();
                    Nickname.Content = HttpGetInfo.GetNickname();
                }));
            });

        }
        private void LoadUserHistory()
        {
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    var history = HttpGetInfo.GetMarketHistory();
                    history.data.OrderBy(o => o.time);
                    History_LB.ItemsSource = history.data;
                }));
            });
        }
        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            UpdateStatus();
            if (HttpGetInfo.TradeRequesTake() == true)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Notification("Accept trade on website");
                    HttpGetInfo.TelegramNotication("Accept trade on website");
                }));
            }

            if (HttpGetInfo.TradeRequestGive() == true)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Notification("Accept trade on website");
                    HttpGetInfo.TelegramNotication("Accept trade on website");
                }));
            };
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
                Task.Run(() =>
                {
                    var items = HttpGetInfo.GetSteamInventory();

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        InventoryLB.Items.Clear();

                        for (int i = 0; i <= items.items.Count - 1; i++)
                        {
                            InventoryLB.Items.Add(items.items[i].market_hash_name
                                + " / " + items.items[i].id);
                        }
                    }));
                });              
            }
            else
            {
                Task.Run(() =>
                {
                    var items = HttpGetInfo.GetItems();

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        ItemsLB.Items.Clear();

                        for (int i = 0; i <= items.items.Count - 1; i++)
                        {
                            ItemsLB.Items.Add(items.items[i].market_hash_name + " / "
                                + items.items[i].price + " "
                                + items.items[i].currency
                                + " / " + items.items[i].item_id);
                        }
                    }));
                });
            }

        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            HttpGetInfo.SetSell(current_item, Sell_Price.Text, HttpGetInfo.Market_currency);
            //MessageBox.Show(sell.success + sell.item_id);
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
                ItemInfo.DataContext = price.data;
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
                ItemInfo.DataContext = price.data;
                Item_Image.Source = HttpGetInfo.GetImage(current_sell_item);

            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            HttpGetInfo.SetPrice(current_sell_item, "0", HttpGetInfo.Market_currency);
            //MessageBox.Show(update.success + update.error);
            ListUpdate(1);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            HttpGetInfo.SetPrice(current_sell_item, Update_Price.Text, HttpGetInfo.Market_currency);
            //MessageBox.Show(update.success + update.error);
            ListUpdate(1);
            Update.IsEnabled = false;
        }

        private void Sell_Price_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            Sell.IsEnabled = true;
        }

        private void Update_Price_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            Update.IsEnabled = true;
        }

    }
}
