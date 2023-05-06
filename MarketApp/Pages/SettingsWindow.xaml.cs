using AdonisUI.Controls;
using MarketCore.Data;
using MarketCore.Utills;
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
            settingsInfo.AutoLoad = AutoLoad.IsChecked ?? true;
            settingsInfo.AutoTray = AutoTray.IsChecked ?? false;
            settingsInfo.TelegramNotification = TelegramNotification.IsChecked ?? false;
            settingsInfo.WindowsNotification = WindowsNotification.IsChecked ?? true;

            IOoperation.SaveSettings(settingsInfo);
            this.Close();
        }

        private void ChangeConfig_Click(object sender, RoutedEventArgs e)
        {
            var conf = new ConfigPage();
            conf.ShowDialog();
        }
    }
}
