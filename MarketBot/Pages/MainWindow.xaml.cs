using AdonisUI;
using AdonisUI.Controls;
using MarketApp.Pages;
using MarketBot.API;
using MarketBot.Notication;
using MarketBot.Parsing;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using static MarketBot.Date.MarketDate;

namespace MarketBot
{
    public partial class MainWindow : AdonisWindow
    {
        private readonly System.Timers.Timer aTimer, bTimer;
        private bool _isDark = true;
        private static NotifyIcon _notifyIcon = Notification.CreateNoti();

        public MainWindow()
        {
            InitializeComponent();
            ParseConfig.ReadConfig();
            UpdateStatusAsync();
            LoadUserInfo();
            MarketAPI.UpdateInventoryAsync();

            aTimer = new (180_000);
            aTimer.Elapsed += (o, e) => UpdateStatusAsync();
            aTimer.Enabled = true;

            bTimer = new (40_000);
            bTimer.Elapsed += (o, e) => CheckTradeAsync();
            bTimer.Enabled = true;

            _notifyIcon.MouseDoubleClick += MyNotifyIcon_MouseDoubleClick;
            _notifyIcon.MouseClick += MyNotifyIcon_MouseClick;

            ParseConfig.ApplySettings(this, _notifyIcon);

        }

        private void MyNotifyIcon_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            _notifyIcon.Visible = false;
            this.ShowInTaskbar = true;
        }

        private void MyNotifyIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _notifyIcon.ContextMenuStrip = new ContextMenuStrip();
                _notifyIcon.ContextMenuStrip.Items.Add("Exit");
                _notifyIcon.ContextMenuStrip.Items[0].Click += (o, e) => { _notifyIcon.Dispose(); this.Close(); };
            }
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
            if (WindowState != WindowState.Minimized)
            {
                e.Cancel = true;
                _notifyIcon.Visible = true;
                MainFrame.Source = null;
                this.ShowInTaskbar = false;
                Notification.WindowNotificationAsync("Application minimized to tray.");
                this.WindowState = WindowState.Minimized;
                this.Hide();
            }
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
