using MarketApp.Date;
using MarketBot.Parsing;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static MarketApp.Date.UserModel;

namespace MarketBot.API
{
    class SteamAPI
    {
        public static async Task<BitmapImage> GetAvatarAsync()
        {
            try
            {

                string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={Config.Steam_API_Key}&steamids={Config.SteamId32}";

                User user_Date = JsonConvert.DeserializeObject<User>(await MarketAPI.GetResponseAsync(actionUrl));

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri($"{user_Date.Response.Players.First().Avatarfull}"); ;
                bitmapImage.EndInit();

                return bitmapImage;
            }
            catch { return null; }

        }
        public static BitmapImage GetImage(string item_name, string? wear = null)
        {
            string Image_Url;

            if (wear == null)
                Image_Url = $"https://cdn.csgo.com//item/{item_name}/300.png";
            else
                Image_Url = $"https://cdn.csgo.com//item/{item_name} ({wear})/300.png";

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(Image_Url);
            bitmapImage.EndInit();

            return bitmapImage;
        }
        public static async Task<string> GetNicknameAsync()
        {
            try
            {
                string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={Config.Steam_API_Key}&steamids={Config.SteamId32}";

                User user_Date = JsonConvert.DeserializeObject<User>(await MarketAPI.GetResponseAsync(actionUrl));
                return user_Date.Response.Players.First().Personaname;
            }
            catch { return string.Empty; }
        }
    }
}
