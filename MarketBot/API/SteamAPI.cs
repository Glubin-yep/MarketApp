using MarketBot.Parsing;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static MarketApp.Date.UserDate;
using static MarketBot.Date.MarketDate;

namespace MarketBot.API
{
    class SteamAPI
    {
        public static string StemaId32 { get; set; }
        public static string Steam_API_Key { get; set; }

        public static async Task<BitmapImage> GetAvatarAsync()
        {
            string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={Steam_API_Key}&steamids={StemaId32}";

            User user_Date = JsonConvert.DeserializeObject<User>(await MarketAPI.GetResponseAsync(actionUrl));

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri($"{user_Date.Response.Players.First().Avatarfull}"); ;
            bitmapImage.EndInit();

            return bitmapImage;

        }
        public static BitmapImage GetImage(string item_name, string? wear = null)
        {
            string id_name = DateParsing.Get_Id_Name(item_name, "name");
            string Image_Url;

            if (wear == null)
                Image_Url = $"https://cdn.csgo.com//item/{id_name}/300.png";
            else
                Image_Url = $"https://cdn.csgo.com//item/{id_name} ({wear})/300.png";

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(Image_Url);
            bitmapImage.EndInit();

            return bitmapImage;
        }
        public static async Task<string> GetNicknameAsync()
        {
            string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={Steam_API_Key}&steamids={StemaId32}";

            User user_Date = JsonConvert.DeserializeObject<User>(await MarketAPI.GetResponseAsync(actionUrl));
            return user_Date.Response.Players.First().Personaname;

        }
    }
}
