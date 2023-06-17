using AdonisUI;
using AdonisUI.Controls;
using MarketApp.Notification;
using MarketApp.Pages;
using MarketCore.API;
using MarketCore.API.MarketAPI;
using MarketCore.Data;
using MarketCore.Utills;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace MarketApp
{
    public partial class MainWindow : AdonisWindow
    {
        private readonly System.Timers.Timer aTimer, bTimer;
        private bool _isDark = true;

        public MainWindow()
        {

            InitializeComponent();

            ReadConfig();
            UpdateStatusAsync();
            LoadUserInfo();

            MarketAPI.Instance.UpdateInventoryAsync();

            aTimer = new(180_000);
            aTimer.Elapsed += (o, e) => UpdateStatusAsync();
            aTimer.Enabled = true;

            bTimer = new(40_000);
            bTimer.Elapsed += (o, e) => CheckTradeAsync();
            bTimer.Enabled = true;

            TrayNotification.MyNotifyIcon.MouseDoubleClick += MyNotifyIcon_MouseDoubleClick;
            TrayNotification.MyNotifyIcon.MouseClick += MyNotifyIcon_MouseClick;

            ApplySettings(this);

        }

        private static void ApplySettings(MainWindow mainWindow)
        {
            Settings settingsInfo = MarketCore.Data.Settings.ReadSettings();

            if (settingsInfo.AutoLoad == true)
            {
                Regedit.AddToAutoLoad();
            }
            else
            {
                Regedit.RemoveFromAutoLoad();
            }

            if (settingsInfo.AutoTray == true)
            {
                TrayNotification.CloseToTray(mainWindow);
            }
        }

        private static void ReadConfig()
        {
            if (Config.ReadConfig() == false)
            {
                WindowsNotification.DisplayInfo("Entry Steam API or StemaId32 or Market API");
                var entry = new ConfigPage();
                entry.ShowDialog();
            }
        }

        private void MyNotifyIcon_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            TrayNotification.OpenFromTray(this);
        }

        private void MyNotifyIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            TrayNotification.OpenContextMenuInTray(e);
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            ResourceLocator.SetColorScheme(System.Windows.Application.Current.Resources, _isDark ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme);

            if (_isDark)
            {
                Theme.SolidIcon = Meziantou.WpfFontAwesome.FontAwesomeSolidIcon.Moon;
                Theme.Foreground = System.Windows.Media.Brushes.Black;
            }
            else
            {
                Theme.SolidIcon = Meziantou.WpfFontAwesome.FontAwesomeSolidIcon.Sun;
                Theme.Foreground = System.Windows.Media.Brushes.White;
            }

            _isDark = !_isDark;
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("OrderPage.xaml", UriKind.RelativeOrAbsolute);
        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("SellPage.xaml", UriKind.RelativeOrAbsolute);
        }

        private void Table_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("TablePage.xaml", UriKind.RelativeOrAbsolute);
        }


        private void AdonisWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            TrayNotification.CloseToTray(this);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settings = new SettingsWindow();
            settings.ShowDialog();
        }

        private void LoadUserInfo()
        {
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(new Action(async () =>
                {
                    Money.Content = await MarketAPI.Instance.GetMoneyAsync();
                    Nickname.Content = await SteamAPI.GetNicknameAsync();
                    Photo.Source = new BitmapImage(new Uri(await SteamAPI.GetAvatarUrlAsync()));

                }));
            });
        }

        private async void CheckTradeAsync()
        {
            LoadUserInfo();
            if (await MarketAPI.Instance.GetTradeRequesTakeAsync() == true || await MarketAPI.Instance.GetTradeRequestGiveAsync() == true)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    WindowsNotification.WindowNotificationAsync("Accept trade on website");
                    TelegramNotification.TelegramNotificationAsync("Accept trade on website");
                }));
            }
        }

        private async void UpdateStatusAsync()
        {
            bool status = await MarketAPI.Instance.GetPingAsync();
            if (status == true)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Status.Content = "Connected :)";
                    Spinner1.Visibility = Visibility.Collapsed;
                }));
            }
        }
    }
}
