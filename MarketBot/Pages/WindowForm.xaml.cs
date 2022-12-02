using AdonisUI.Controls;
using MarketBot.API;
using MarketBot.Notication;
using MarketBot.Parsing;

namespace Pages
{
    public partial class WindowForm : AdonisWindow
    {
        private readonly object? _current_order;
        public WindowForm()
        {
            InitializeComponent();
        }
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

        private async void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var status = await MarketAPI.SetOrderAsync(MarketName.Text, "(" + Wear_list.Text + ")", Count.Text, Price.Text.Replace(".", ""));

            if (status.Success == true)
            {
                Notification.DisplayInfo("Order successfully added :)");
                this.Close();
            }
            else
                Notification.DisplayInfo("It is not possible to add this order :(");
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                Image.Source = SteamAPI.GetImage(MarketName.Text, Wear_list.Text);
            }
            catch { }
        }
        private void Wear_list_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                Image.Source = SteamAPI.GetImage(MarketName.Text, Wear_list.Text);
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
