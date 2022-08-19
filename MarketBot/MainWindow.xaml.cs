﻿using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MarketBot
{
    public partial class MainWindow : Window
    {
        private Timer aTimer;
        

        public MainWindow()
        {
            InitializeComponent();

            var balans = JsonConvert.DeserializeObject<User_Date.Balans>(HttpGetInfo.GetMoney());
            Money.Content = balans.money.ToString();
            var user = JsonConvert.DeserializeObject<User_Date.User>(HttpGetInfo.GetAvatar());

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri($"{user.response.players.First().avatarfull}"); ;
            bitmapImage.EndInit();
            Photo.Source = bitmapImage;

            Nickname.Content = user.response.players.First().personaname;

            UpdateStatus();
            aTimer = new Timer(60000);           
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
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
                    var status = JsonConvert.DeserializeObject<User_Date.Ping>(HttpGetInfo.GetPing());
                    if (status.ping == "pong")
                        TradeStatus.Content = "Connected";
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
            if(mode == 0)
            {
                InventoryLB.Items.Clear();
                var items = JsonConvert.DeserializeObject<User_Date.Inventory>(HttpGetInfo.GetSteamInventory());
                for (int i = 0; i <= items.items.Count - 1; i++)
                {
                    InventoryLB.Items.Add(items.items[i].market_hash_name + " id:" + items.items[i].id);
                }
            }
            else
            {
                ItemsLB.Items.Clear();
                var items = JsonConvert.DeserializeObject<User_Date.Items>(HttpGetInfo.GetItems());
                for (int i = 0; i <= items.items.Count - 1; i++)
                {
                    ItemsLB.Items.Add(items.items[i].market_hash_name + " " + items.items[i].price + " " + items.items[i].currency + " id:" + items.items[i].item_id);
                }
            }
            
        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            var sell = JsonConvert.DeserializeObject<User_Date.Sell>(HttpGetInfo.SetSell(HttpGetInfo.GetId(User_Date.current_item),Sell_Price.Text + "00","RUB"));
            MessageBox.Show(sell.success + sell.item_id);
            ListUpdate(0);
            ListUpdate(1);
            Sell.IsEnabled = false;
        }

        private void InventoryLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sell_Price.IsEnabled = true;
            Sell_Price.Text = "";
            if(e.AddedItems.Count >= 1)
                User_Date.current_item = e.AddedItems[0].ToString();
            
        }

        private void ItemsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Remove.IsEnabled = true;
            Update.IsEnabled = true;
            Update_Price.IsEnabled = true;
            Update_Price.Text = "";

            if (e.AddedItems.Count >= 1)
            {
                User_Date.current_sell_item = e.AddedItems[0].ToString();
            }
                    

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var update = JsonConvert.DeserializeObject<User_Date.Update>(HttpGetInfo.SetPrice(HttpGetInfo.GetId(User_Date.current_sell_item),"0","RUB"));
            MessageBox.Show(update.success + update.error);
            ListUpdate(1);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var update = JsonConvert.DeserializeObject<User_Date.Update>(HttpGetInfo.SetPrice(HttpGetInfo.GetId(User_Date.current_sell_item), Update_Price.Text + "00", "RUB"));
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
    }
}
