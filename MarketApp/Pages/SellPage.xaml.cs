using MarketBot.Notication;
using MarketBot.Parsing;
using MarketLIB;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using static MarketLIB.Models.MarketModel;

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
                    var history = await MarketAPI.Instance.GetMarketHistoryAsync();
                    History_LB.ItemsSource = history.Data;
                }));
            });
        }

        private async void Iteams_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Spinner1.Visibility = System.Windows.Visibility.Visible;
            await ListUpdate(1);
            Spinner1.Visibility = System.Windows.Visibility.Collapsed;
        }

        private async void Inventory_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Spinner2.Visibility = System.Windows.Visibility.Visible;
            await ListUpdate(0);
            Spinner2.Visibility = System.Windows.Visibility.Collapsed;
        }

        public async Task<bool> ListUpdate(int mode) // 0 == inventory // 1 == Items
        {
            if (mode == 0)
            {
                await Task.Run(async () =>
                {
                    this.Dispatcher.Invoke(new Action(async () =>
                    {
                        var items = await MarketAPI.Instance.GetSteamInventoryAsync();

                        if (items.Items.Count == 0)
                            Notification.DisplayInfo("Refresh inventory again and try again, data could not be loaded from http://steamcommunity.com/\r\nReason: Unstable operation of the Steam server. Please try again later.");

                        InventoryLB.ItemsSource = items.Items;
                    }));
                    await Task.Delay(550);
                });
                return true;
            }
            else
            {
                await Task.Run(async () =>
                {
                    this.Dispatcher.Invoke(new Action(async () =>
                    {
                        var items = await MarketAPI.Instance.GetItemsAsync();

                        if (items.Items.Count == 0)
                            Notification.DisplayInfo("You are not selling any items");

                        if (items.Success == false)
                            Notification.DisplayInfo("Refresh inventory again and try again, data could not be loaded from https://market.csgo.com/");

                        ItemsLB.ItemsSource = items.Items;
                    }));
                    await Task.Delay(550);
                });
            }
            return true;
        }

        private async void Sell_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var sell = await MarketAPI.Instance.SetSellAsync(Current_sell_item_id, Sell_Price.Text, Market_currency);

            if (sell.Success == true)
                Notification.DisplayInfo("Item is successfully add for sale");
            else
                Notification.DisplayInfo("Item not added for sale");

            await ListUpdate(0);
            await ListUpdate(1);
            Sell.IsEnabled = false;
        }

        private void InventoryLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sell_Price.IsEnabled = true;
            Sell_Price.Text = "";

            if (e.AddedItems.Count >= 1)
            {
                var nameOfProperty = "Id";
                var propertyInfo = e.AddedItems[0]?.GetType().GetProperty(nameOfProperty);
                Current_sell_item_id = propertyInfo?.GetValue(e.AddedItems[0], null)?.ToString();
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
                var nameOfProperty = "Item_id";
                var propertyInfo = e.AddedItems[0]?.GetType().GetProperty(nameOfProperty);
                Current_sell_item_id = propertyInfo?.GetValue(e.AddedItems[0], null)?.ToString();
            }
        }

        private async void Remove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var update = await MarketAPI.Instance.SetPriceAsync(Current_sell_item_id, "0", Market_currency);

            if (update.Success == true)
                Notification.DisplayInfo("Item successfully deleted");
            else
                Notification.DisplayInfo("Item not deleted");

            await ListUpdate(1);
        }

        private async void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var price = await MarketAPI.Instance.SetPriceAsync(Current_sell_item_id, Update_Price.Text, Market_currency);

            if (price.Success == true)
                Notification.DisplayInfo("The item price has been successfully updated");
            else
                Notification.DisplayInfo("The product price was not successfully updated");

            await ListUpdate(1);
            Update.IsEnabled = false;
        }

        private void Sell_Price_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            CustomValidation.ValidationTextBox(sender, e);
            Sell.IsEnabled = true;
        }

        private void Update_Price_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            CustomValidation.ValidationTextBox(sender, e);
            Update.IsEnabled = true;
        }
    }
}
