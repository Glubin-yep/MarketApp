using AdonisUI.Controls;
using MarketBot.API;
using MarketBot.Parsing;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using static MarketBot.Date.MarketDate;

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
                    var history = await MarketAPI.GetMarketHistoryAsync();
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

        public async Task<bool> ListUpdate(int mode) // 0 == inventory // 1 == Items // its very bad // TODO rewrite
        {
            if (mode == 0)
            {
                var task = Task.Run(async () =>
                {
                    var items = await MarketAPI.GetSteamInventoryAsync();

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        InventoryLB.Items.Clear();

                        for (int i = 0; i <= items.Items.Count - 1; i++)
                        {
                            InventoryLB.Items.Add(items.Items[i].Market_hash_name
                                + " / " + items.Items[i].Id);
                        }
                        InventoryLB.Items.SortDescriptions.Add(
                                new System.ComponentModel.SortDescription("",
                                System.ComponentModel.ListSortDirection.Ascending));
                    }));
                    return true;
                });
                return await task;
            }
            else
            {
                await Task.Run(async () =>
                {
                    this.Dispatcher.Invoke(new Action(async () =>
                    {
                        var items = await MarketAPI.GetItemsAsync();
                        ItemsLB.ItemsSource = items.Items;
                    }));
                    await Task.Delay(100);
                });
                return true;
            }

        }

        private async void Sell_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var sell = await MarketAPI.SetSellAsync(Current_item, Sell_Price.Text, Market_currency);

            if (sell.Success == true)
                MessageBox.Show("Item is successfully add for sale", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Item not added for sale", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            await ListUpdate(0);
            await ListUpdate(1);
            Sell.IsEnabled = false;
        }

        private async void InventoryLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sell_Price.IsEnabled = true;
            Sell_Price.Text = "";
            Min_Price.Visibility = System.Windows.Visibility.Visible;

            if (e.AddedItems.Count >= 1)
            {
                Current_item = e.AddedItems[0]?.ToString();
                var price = await MarketAPI.GetMarketPriceAsync(Current_item);
                ItemInfo.DataContext = price.Data;
                Item_Image.Source = SteamAPI.GetImage(Current_item);
            }
        }

        private void ItemsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Remove.IsEnabled = true;
            Update.IsEnabled = true;
            Update_Price.IsEnabled = true;
            Update_Price.Text = "";
            Min_Price.Visibility = System.Windows.Visibility.Visible;

            if (e.AddedItems.Count >= 1)
            {
                var nameOfProperty = "Item_id";
                var propertyInfo = e.AddedItems[0]?.GetType().GetProperty(nameOfProperty);
                Current_sell_item_id = propertyInfo?.GetValue(e.AddedItems[0], null)?.ToString();
            }
        }

        private async void Remove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var update = await MarketAPI.SetPriceAsync(Current_sell_item_id, "0", Market_currency);

            if (update.Success == true)
                MessageBox.Show("Item successfully deleted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Item not deleted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            await ListUpdate(1);
        }

        private async void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var price = await MarketAPI.SetPriceAsync(Current_sell_item_id, Update_Price.Text, Market_currency);

            if (price.Success == true)
                MessageBox.Show("The item price has been successfully updated", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("The product price was not successfully updated", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            await ListUpdate(1);
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
