using AdonisUI.Controls;
using MarketApp.Date;
using MarketApp.Utills;
using System.Windows;

namespace MarketApp.Pages
{
    public partial class SettingsWindow : AdonisWindow
    {
        private readonly Settings settingsInfo = Settings.ReadSettings();
        public SettingsWindow()
        {
            InitializeComponent();

            AutoLoad.IsChecked = settingsInfo.AutoLoad;
            AutoTray.IsChecked = settingsInfo.AutoTray;
            TelegramNotification.IsChecked = settingsInfo.TelegramNotification;
            WindowsNotification.IsChecked = settingsInfo.WindowsNotification;
        }

        private void Cancell_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            settingsInfo.AutoLoad = AutoLoad.IsChecked;
            settingsInfo.AutoTray = AutoTray.IsChecked;
            settingsInfo.TelegramNotification = TelegramNotification.IsChecked;
            settingsInfo.WindowsNotification = WindowsNotification.IsChecked;

            IOoperation.SaveSettings(settingsInfo);
            this.Close();
        }
    }
}
