using AdonisUI.Controls;
using MarketBot.API;
using MarketBot.Parsing;
using System.Windows.Media.Imaging;
using System;
using static MarketBot.Date.User_Date;
using System.Windows.Forms;

namespace Pages
{
    /// <summary>
    /// Interaction logic for WindowForm.xaml
    /// </summary>
    public partial class WindowForm : AdonisWindow
    {
        private object current_order_;
        public WindowForm(object current_order)
        {
            InitializeComponent();
            current_order_ = current_order;
        }
        private void AdonisWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (current_order_ != null)
            {
                MarketName.IsEnabled = false;
                Wear_list.IsEnabled = false;
                Add.Content = "Update";

                var nameOfProperty = "hash_name";
                var propertyInfo = current_order_.GetType().GetProperty(nameOfProperty);
                string[] text = propertyInfo.GetValue(current_order_, null).ToString().Split(" (");
                MarketName.Text = text[0];
                Wear_list.Text = text[1].Replace(")","");

                nameOfProperty = "count";
                propertyInfo = current_order_.GetType().GetProperty(nameOfProperty);
                string count = propertyInfo.GetValue(current_order_, null).ToString();
                Count.Text = count;

                nameOfProperty = "price";
                propertyInfo = current_order_.GetType().GetProperty(nameOfProperty);
                string price = propertyInfo.GetValue(current_order_, null).ToString();
                Price.Text = price.Replace(",",".").Replace(" RUB", "");
            }
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var status =  MarketAPI.SetOrder(MarketName.Text, "(" + Wear_list.Text + ")", Count.Text, Price.Text.Replace(".",""));
            AdonisUI.Controls.MessageBox.Show(status.success.ToString(), "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            if(status.success == true)
                this.Close();
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
