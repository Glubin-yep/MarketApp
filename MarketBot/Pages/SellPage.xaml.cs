using MarketBot.API;
using MarketBot.Parsing;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static MarketBot.Date.User_Date;

namespace MarketBot.Pages
{

    public partial class SellPage
    {

        public SellPage()
        {
            InitializeComponent();

            LoadUserHistory();
        }

        private void LoadUserHistory()
        {
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(new Action(async () =>
                {
                    var history = await MarketAPI.GetMarketHistory();
                    History_LB.ItemsSource = history.data;
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

        public  void ListUpdate(int mode) // 0 == inventory // 1 == Items
        {
            if (mode == 0)
            {
                Task.Run(async () =>
                {
                    var items = await MarketAPI.GetSteamInventory();

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
                Task.Run(async () =>
                {
                    var items = await MarketAPI.GetItems();

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

        private async void InventoryLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sell_Price.IsEnabled = true;
            Sell_Price.Text = "";
            Min_Price.Visibility = Visibility.Visible;

            if (e.AddedItems.Count >= 1)
            {
                Current_item = e.AddedItems[0].ToString();
                var price = await MarketAPI.GetMarketPrice(Current_item);
                ItemInfo.DataContext = price.data;
                Item_Image.Source = SteamAPI.GetImage(Current_item);
            }
        }

        private async void ItemsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Remove.IsEnabled = true;
            Update.IsEnabled = true;
            Update_Price.IsEnabled = true;
            Update_Price.Text = "";
            Min_Price.Visibility = Visibility.Visible;

            if (e.AddedItems.Count >= 1)
            {
                Current_sell_item = e.AddedItems[0].ToString();
                var price = await MarketAPI.GetMarketPrice(Current_sell_item);
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
