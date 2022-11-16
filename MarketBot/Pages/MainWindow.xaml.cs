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
using static MarketBot.Date.MarketDate;

namespace MarketBot
{
    public partial class MainWindow : AdonisWindow
    {
        private readonly Timer aTimer, bTimer;
        private bool _isDark = true;
        private readonly System.Windows.Forms.NotifyIcon MyNotifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            DateParsing.ReadConfig();
            UpdateStatusAsync();
            LoadUserInfo();
            MarketAPI.UpdateInventoryAsync();

            aTimer = new Timer(180_000);
            aTimer.Elapsed += (o, e) => UpdateStatusAsync();
            aTimer.Enabled = true;

            bTimer = new Timer(40_000);
            bTimer.Elapsed += (o, e) => CheckTradeAsync();
            bTimer.Enabled = true;

            MyNotifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = new System.Drawing.Icon("MarketApp.ico")
            };

            MyNotifyIcon.MouseDoubleClick += MyNotifyIcon_MouseDoubleClick;
            MyNotifyIcon.MouseClick += MyNotifyIcon_MouseClick;
        }

        private void MyNotifyIcon_MouseClick(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MyNotifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
                MyNotifyIcon.ContextMenuStrip.Items.Add("Exit");
                MyNotifyIcon.ContextMenuStrip.Items[0].Click += (o, e) => { MyNotifyIcon.Dispose(); this.Close(); };
            }
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            ResourceLocator.SetColorScheme(Application.Current.Resources, _isDark ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme);

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

        private void MyNotifyIcon_MouseDoubleClick(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            MyNotifyIcon.Visible = false;
            this.ShowInTaskbar = true;
        }

        private void AdonisWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (WindowState != WindowState.Minimized)
            {
                e.Cancel = true;
                MyNotifyIcon.Visible = true;
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
            LoadUserInfo();
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
