using AdonisUI.Controls;
using MarketApp.Notification;
using MarketApp.Parsing;
using MarketCore.API;
using MarketCore.API.MarketAPI;
using System;
using System.Windows.Media.Imaging;
using static MarketCore.API.MarketAPI.Models.OrdersModel;

namespace Pages
{
    public partial class WindowForm : AdonisWindow
    {
        private readonly Order _current_order;
        public WindowForm()
        {
            InitializeComponent();
        }
        public WindowForm(object current_order)
        {
            InitializeComponent();
            _current_order = (Order)current_order;
            UpdateOrder();
        }

        private void UpdateOrder()
        {
            MarketName.IsEnabled = false;
            Wear_list.IsEnabled = false;
            Add.Content = "Update";

            string[] text = _current_order.Hash_name.Split(" (");
            MarketName.Text = text[0];
            Wear_list.Text = text[1].Replace(")", "");

            Count.Text = _current_order.Count;

            string price = _current_order.Price;
            Price.Text = price.Replace(",", ".").Replace(" RUB", "");
        }

        private async void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var status = await MarketAPI.Instance.SetOrderAsync(MarketName.Text, "(" + Wear_list.Text + ")", Count.Text, Price.Text.Replace(".", ""));

            if (status.Success == true)
            {
                WindowsNotification.DisplayInfo("Order successfully added :)");
                this.Close();
            }
            else
                WindowsNotification.DisplayInfo("It is not possible to add this order :(");
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                var bitmapImage = new BitmapImage
                {
                    UriSource = new Uri(SteamAPI.GetImageUrl(MarketName.Text, Wear_list.Text))
                };
                Image.Source = bitmapImage;
            }
            catch { }
        }
        private void Wear_list_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                var bitmapImage = new BitmapImage
                {
                    UriSource = new Uri(SteamAPI.GetImageUrl(MarketName.Text, Wear_list.Text))
                };
                Image.Source = bitmapImage;
            }
            catch { }
        }

        private void TextBox_PreviewTextInput_Count(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            CustomValidation.ValidationTextBox(sender, e);
        }
        private void TextBox_PreviewTextInput_Price(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            CustomValidation.ValidationTextBox(sender, e);
            Add.IsEnabled = true;
        }
    }
}
