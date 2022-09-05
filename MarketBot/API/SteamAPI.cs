using MarketBot.Parsing;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static MarketBot.Date.User_Date;

namespace MarketBot.API
{
    class SteamAPI
    {
        public static async Task<BitmapImage> GetAvatar()
        {
            string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={Steam_API_Key}&steamids={StemaId32}";

            User user_Date = JsonConvert.DeserializeObject<User>(await MarketAPI.GetAPI(actionUrl));

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri($"{user_Date.response.players.First().avatarfull}"); ;
            bitmapImage.EndInit();

            return bitmapImage;
        }
        public static BitmapImage GetImage(string item_name)
        {
            string Image_Url = $"https://cdn.csgo.com//item/{DateParsing.Get_Id_Name(item_name, "name")}/300.png";

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(Image_Url);
            bitmapImage.EndInit();

            return bitmapImage;
        }
        public static async Task<string> GetNickname()
        {
            string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={Steam_API_Key}&steamids={StemaId32}";

            User user_Date = JsonConvert.DeserializeObject<User>(await MarketAPI.GetAPI(actionUrl));

            return user_Date.response.players.First().personaname;
        }
    }
}
