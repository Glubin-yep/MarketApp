using AdonisUI.Controls;
using MarketApp.Settings;

namespace MarketApp.Pages
{
    public partial class ConfigPage : AdonisWindow
    {
        public ConfigPage()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Config.StemaId32 = SteamId32.Text;
            Config.Market_API_Key = Market_API_Key.Text;
            Config.Steam_API_Key = Steam_API_Key.Text;
            Config.Telegram_User_Id = Telegram_User_Id.Text;

            this.Close();
        }
    }
}
