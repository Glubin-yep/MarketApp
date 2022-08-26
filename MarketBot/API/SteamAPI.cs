using MarketBot.Date;
using MarketBot.Date.QuickType;
using MarketBot.Parsing;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Windows.Media.Imaging;
using static MarketBot.Date.User_Date;

namespace MarketBot.API
{
    class SteamAPI
    {
        public static BitmapImage GetAvatar()
        {
            string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={Steam_API_Key}&steamids={StemaId32}";

            User user_Date = JsonConvert.DeserializeObject<User>(MarketAPI.GetAPI(actionUrl));

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri($"{user_Date.response.players.First().avatarfull}"); ;
            bitmapImage.EndInit();

            return bitmapImage;
        }
        public static BitmapImage GetImage(string item_name)
        {
            string Image_Url = $"https://cdn.csgo.com//item/{DateParsing.Get_Id_Name(item_name, "name")}/150.png";

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(Image_Url);
            bitmapImage.EndInit();

            return bitmapImage;
        }
        public static string GetNickname()
        {
            string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={Steam_API_Key}&steamids={StemaId32}";

            User user_Date = JsonConvert.DeserializeObject<User>(MarketAPI.GetAPI(actionUrl));

            return user_Date.response.players.First().personaname;
        }
        public static dynamic GetAllItems()
        {
            string actionUrl = $"http://csgobackpack.net/api/GetItemsList/v2/";

            var user_Date = JsonConvert.DeserializeObject<dynamic>(MarketAPI.GetAPI(actionUrl));

            return user_Date.ItemsList;
        }
    }
}
