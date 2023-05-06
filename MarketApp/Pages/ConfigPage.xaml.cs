using AdonisUI.Controls;
using MarketApp.Notication;
using MarketCore.Data;
using MarketCore.Utills;

namespace MarketApp.Pages
{
    public partial class ConfigPage : AdonisWindow
    {
        public ConfigPage()
        {
            InitializeComponent();

            SteamId32.Text = Config.SteamId32;
            Market_API_Key.Text = Config.Market_API_Key;
            Steam_API_Key.Text = Config.Steam_API_Key;
            Telegram_User_Id.Text = Config.Telegram_User_Id;
        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Config.SteamId32 = SteamId32.Text;
            Config.Market_API_Key = Market_API_Key.Text;
            Config.Steam_API_Key = Steam_API_Key.Text;
            Config.Telegram_User_Id = Telegram_User_Id.Text;

            if (Config.SteamId32 == string.Empty || Config.Market_API_Key == string.Empty || Config.Steam_API_Key == string.Empty)
            {
                Notification.DisplayInfo("Entry data pls");
            }
            else
            {
                IOoperation.SaveConfig();
                this.Close();
                System.Windows.Forms.Application.Restart();
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
