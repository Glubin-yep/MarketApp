using AdonisUI.Controls;
using MarketBot.API;
using MarketBot.Parsing;
using System;
using System.Windows.Media.Imaging;

namespace Pages
{
    public partial class WindowForm : AdonisWindow
    {
        private object _current_order;
        public WindowForm(object current_order)
        {
            InitializeComponent();
            _current_order = current_order;
        }
        private void AdonisWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_current_order != null)
            {
                MarketName.IsEnabled = false;
                Wear_list.IsEnabled = false;
                Add.Content = "Update";

                var nameOfProperty = "hash_name";
                var propertyInfo = _current_order.GetType().GetProperty(nameOfProperty);
                string[] text = propertyInfo.GetValue(_current_order, null).ToString().Split(" (");
                MarketName.Text = text[0];
                Wear_list.Text = text[1].Replace(")", "");

                nameOfProperty = "count";
                propertyInfo = _current_order.GetType().GetProperty(nameOfProperty);
                string count = propertyInfo.GetValue(_current_order, null).ToString();
                Count.Text = count;

                nameOfProperty = "price";
                propertyInfo = _current_order.GetType().GetProperty(nameOfProperty);
                string price = propertyInfo.GetValue(_current_order, null).ToString();
                Price.Text = price.Replace(",", ".").Replace(" RUB", "");
            }
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var status = MarketAPI.SetOrder(MarketName.Text, "(" + Wear_list.Text + ")", Count.Text, Price.Text.Replace(".", ""));

            if (status.success == true)
            {
                MessageBox.Show("Order successfully added :)", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
                MessageBox.Show("It is not possible to add this order :(", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            GetImage();
        }
        private void Wear_list_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            GetImage();
        }

        private void TextBox_PreviewTextInput_Count(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            CustomValidation.Validation_TextBox(sender, e);
        }
        private void TextBox_PreviewTextInput_Price(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            CustomValidation.Validation_TextBox(sender, e);
            Add.IsEnabled = true;
        }
        private void GetImage()
        {
            try
            {
                string Image_Url = $"https://cdn.csgo.com//item/{MarketName.Text} ({Wear_list.Text})/300.png";

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(Image_Url);
                bitmapImage.EndInit();

                Image.Source = bitmapImage;
            }
            catch
            {

            }
        }
    }
}
