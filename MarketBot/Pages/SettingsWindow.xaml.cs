using AdonisUI.Controls;
using MarketApp.Settings;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace MarketApp.Pages
{
    public partial class SettingsWindow : AdonisWindow
    {
        private readonly Settings.Settings settingsInfo = Settings.Settings.ReadSettings();
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

            var json = JsonConvert.SerializeObject(settingsInfo);
            File.WriteAllText(@"Settings.json", json);
            this.Close();
        }
    }
}
