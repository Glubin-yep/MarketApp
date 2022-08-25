using AdonisUI.Controls;
using MarketBot.API;
using static MarketBot.Date.User_Date;
using MarketBot.Notication;
using MarketBot.Parsing;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace MarketBot
{
    public partial class MainWindow : AdonisWindow
    {
        private readonly Timer aTimer;

        public MainWindow()
        {
            InitializeComponent();
            DateParsing.ReadConfig();
            LoadUserInfo();
            LoadUserHistory();

            aTimer = new Timer(90000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;

        }
        private void LoadUserInfo()
        {
            UpdateStatus();
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Money.Content = MarketAPI.GetMoney() + " " + Market_currency;
                    Photo.Source = SteamAPI.GetAvatar();
                    Nickname.Content = SteamAPI.GetNickname();
                }));
            });

        }
        private void LoadUserHistory()
        {
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    var history = MarketAPI.GetMarketHistory();
                    History_LB.ItemsSource = history.data;
                }));
            });
        }
        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            UpdateStatus();
            if (MarketAPI.TradeRequesTake() == true)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Notification.WindowNotification("Accept trade on website");
                    Notification.TelegramNotication("Accept trade on website");
                }));
            }

            if (MarketAPI.TradeRequestGive() == true)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Notification.WindowNotification("Accept trade on website");
                    Notification.TelegramNotication("Accept trade on website");
                }));
            };
        }

        private void UpdateStatus()
        {
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    bool status = MarketAPI.GetPing();
                    if (status == true)
                        TradeStatus.Content = "Connected :)";
                }));
            });
        }

        private async void Iteams_Button_Click(object sender, RoutedEventArgs e)
        {
            Spinner1.Visibility = Visibility.Visible;
            ListUpdate(1);
            await Task.Delay(2000);
            Spinner1.Visibility = Visibility.Collapsed;
        }

        private async void Inventory_Button_Click(object sender, RoutedEventArgs e)
        {
            Spinner2.Visibility = Visibility.Visible;
            ListUpdate(0);
            await Task.Delay(2000);
            Spinner2.Visibility = Visibility.Collapsed;
        }

        public void ListUpdate(int mode) // 0 == inventory // 1 == Items
        {
            if (mode == 0)
            {
                Task.Run(() =>
                {
                    var items = MarketAPI.GetSteamInventory();

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        InventoryLB.Items.Clear();

                        for (int i = 0; i <= items.items.Count - 1; i++)
                        {
                            InventoryLB.Items.Add(items.items[i].market_hash_name
                                + " / " + items.items[i].id);
                        }
                        InventoryLB.Items.SortDescriptions.Add(
                                new System.ComponentModel.SortDescription("",
                                System.ComponentModel.ListSortDirection.Ascending));
                    }));
                });
            }
            else
            {
                Task.Run(() =>
                {
                    var items = MarketAPI.GetItems();

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
                        ItemsLB.Items.SortDescriptions.Add(
                                new System.ComponentModel.SortDescription("",
                                System.ComponentModel.ListSortDirection.Ascending));
                    }));
                });
            }

        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            MarketAPI.SetSell(Current_item, Sell_Price.Text, Market_currency);
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
                Current_item = e.AddedItems[0].ToString();
                var price = MarketAPI.GetMarketPrice(Current_item);
                ItemInfo.DataContext = price.data;
                Item_Image.Source = SteamAPI.GetImage(Current_item);
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
                Current_sell_item = e.AddedItems[0].ToString();
                var price = MarketAPI.GetMarketPrice(Current_sell_item);
                ItemInfo.DataContext = price.data;
                Item_Image.Source = SteamAPI.GetImage(Current_sell_item);

            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            MarketAPI.SetPrice(Current_sell_item, "0", Market_currency);
            //MessageBox.Show(update.success + update.error);
            ListUpdate(1);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            MarketAPI.SetPrice(Current_sell_item, Update_Price.Text, Market_currency);
            //MessageBox.Show(update.success + update.error);
            ListUpdate(1);
            Update.IsEnabled = false;
        }

        private void Sell_Price_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            CustomValidation.Validation_TextBox(sender, e);
            Sell.IsEnabled = true;
        }

        private void Update_Price_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            CustomValidation.Validation_TextBox(sender, e);
            Update.IsEnabled = true;
        }
    }
}
