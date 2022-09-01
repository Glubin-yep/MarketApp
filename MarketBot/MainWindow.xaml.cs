using AdonisUI;
using AdonisUI.Controls;
using MarketBot.API;
using MarketBot.Notication;
using MarketBot.Parsing;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using static MarketBot.Date.User_Date;

namespace MarketBot
{
    public partial class MainWindow : AdonisWindow
    {
        private readonly Timer aTimer;
        private bool _isDark = true;

        public MainWindow()
        {
            InitializeComponent();
            DateParsing.ReadConfig();
            UpdateStatus();
            LoadUserInfo();
            MarketAPI.UpdateInventory();

            aTimer = new Timer(90000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            ResourceLocator.SetColorScheme(Application.Current.Resources, _isDark ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme);

            if (_isDark)
            {
                Theme.Icon = FontAwesome.WPF.FontAwesomeIcon.MoonOutline;
                Theme.Foreground = System.Windows.Media.Brushes.Black;
            }
            else
            {
                Theme.Icon = FontAwesome.WPF.FontAwesomeIcon.SunOutline;
                Theme.Foreground = System.Windows.Media.Brushes.White;
            }

            _isDark = !_isDark;
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("Pages/OrderPage.xaml", UriKind.RelativeOrAbsolute);
        }
        
        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("Pages/SellPage.xaml", UriKind.RelativeOrAbsolute);
        }
       
        private void Table_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("Pages/TablePage.xaml", UriKind.RelativeOrAbsolute);
        }

        private async void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            UpdateStatus();
            LoadUserInfo();
            if (await MarketAPI.TradeRequesTake() == true)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Notification.WindowNotification("Accept trade on website");
                    Notification.TelegramNotication("Accept trade on website");
                }));
            }

            if (await MarketAPI.TradeRequestGive() == true)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Notification.WindowNotification("Accept trade on website");
                    Notification.TelegramNotication("Accept trade on website");
                }));
            };
        }

        private static async void UpdateStatus()
        {
            bool status = await MarketAPI.GetPing();
            if (status == true)
                TradeStatus = true;
        }
        private void LoadUserInfo()
        {
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(new Action(async () =>
                {
                    Money.Content = await MarketAPI.GetMoney() + " " + Market_currency;
                    Photo.Source = await SteamAPI.GetAvatar();
                    Nickname.Content = await SteamAPI.GetNickname();

                    if (TradeStatus == true)
                        Status.Content = "Connected :)";
                }));
            });

        }
    }
}
