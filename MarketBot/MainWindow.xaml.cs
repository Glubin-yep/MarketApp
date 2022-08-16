using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MarketBot
{
    public partial class MainWindow : Window
    {
        private Timer aTimer;

        public MainWindow()
        {
            InitializeComponent();

            var balans = JsonConvert.DeserializeObject<User_Date.Balans>(HttpGetInfo.GetMoney());
            Money.Content = balans.money.ToString();
            var user = JsonConvert.DeserializeObject<User_Date.User>(HttpGetInfo.GetAvatar());

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri($"{user.response.players.First().avatarfull}"); ;
            bitmapImage.EndInit();
            Photo.Source = bitmapImage;

            Nickname.Content = user.response.players.First().personaname;

            // Create a timer with 30 seconds interval.
            aTimer = new System.Timers.Timer(20000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(new Action(async () =>
                {
                        var status = JsonConvert.DeserializeObject<User_Date.Ping>(HttpGetInfo.GetPing());
                        TradeStatus.Content = status.ping;                        
                    
                }));
            });
        }
       
    }
}
