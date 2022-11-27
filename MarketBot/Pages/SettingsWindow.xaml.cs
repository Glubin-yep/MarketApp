using AdonisUI.Controls;
using MarketApp.Settings;
using MarketBot.Notication;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace MarketApp.Pages
{
    public partial class SettingsWindow : AdonisWindow
    {
        private SettingsInfo settingsInfo = new();
        public SettingsWindow()
        {
            InitializeComponent();

            settingsInfo = JsonConvert.DeserializeObject<SettingsInfo>(File.ReadAllText(@"Settings.json"));

            AutoLoad.IsChecked = settingsInfo.AutoLoad;
            AutoTray.IsChecked = settingsInfo.AutoTray;
            TelegramNotification.IsChecked = settingsInfo.TelegramNotification;
            WindowsNotification.IsChecked = settingsInfo.WindowsNotification;
        }

        private void Cancell_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    string BaseDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            //    Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            //    key.DeleteValue("MarketApp", false);
            //}
            //catch { }
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
