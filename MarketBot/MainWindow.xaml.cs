using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarketBot
{
    public partial class MainWindow : Window
    {
       
        

        public MainWindow()
        {
            InitializeComponent();

            var balans = JsonConvert.DeserializeObject<Balans>(HttpGetInfo.GetMoney());
            Money.Content = balans.money.ToString();
            var user = JsonConvert.DeserializeObject<User>(HttpGetInfo.GetAvatar());

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri($"{user.response.players.First().avatarfull}"); ;
            bitmapImage.EndInit();
            Photo.Source = bitmapImage;

            Nickname.Content = user.response.players.First().personaname;

        }

          private void Test_Click(object sender, RoutedEventArgs e)

          {
            
        }

        
    }
}
