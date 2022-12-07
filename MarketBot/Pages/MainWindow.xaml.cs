using AdonisUI;
using AdonisUI.Controls;
using MarketApp.Pages;
using MarketApp.Settings;
using MarketBot.API;
using MarketBot.Notication;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static MarketBot.Date.MarketModel;

namespace MarketBot
{
    public partial class MainWindow : AdonisWindow
    {
        private readonly System.Timers.Timer aTimer, bTimer;
        private bool _isDark = true;

        public MainWindow()
        {
            InitializeComponent();

            Config.ReadConfig();
            UpdateStatusAsync();
            LoadUserInfo();
            MarketAPI.UpdateInventoryAsync();

            aTimer = new(180_000);
            aTimer.Elapsed += (o, e) => UpdateStatusAsync();
            aTimer.Enabled = true;

            bTimer = new(40_000);
            bTimer.Elapsed += (o, e) => CheckTradeAsync();
            bTimer.Enabled = true;

            Tray.MyNotifyIcon.MouseDoubleClick += MyNotifyIcon_MouseDoubleClick;
            Tray.MyNotifyIcon.MouseClick += MyNotifyIcon_MouseClick;

            MarketApp.Settings.Settings.ApplySettings(this);

        }

        private void MyNotifyIcon_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            Tray.OpenFromTray(this);
        }

        private void MyNotifyIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            Tray.OpenContextMenuInTray(this, e);
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
            Tray.CloseToTray(this);
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
                    Money.Content = await MarketAPI.GetMoneyAsync() + " " + Market_currency;
                    Photo.Source = await SteamAPI.GetAvatarAsync();
                    Nickname.Content = await SteamAPI.GetNicknameAsync();
                }));
            });
        }

        private async void CheckTradeAsync()
        {
            LoadUserInfo();
            if (await MarketAPI.GetTradeRequesTakeAsync() == true || await MarketAPI.GetTradeRequestGiveAsync() == true)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Notification.WindowNotificationAsync("Accept trade on website");
                    Notification.TelegramNotificationAsync("Accept trade on website");
                }));
            }
        }

        private async void UpdateStatusAsync()
        {
            bool status = await MarketAPI.GetPingAsync();
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
