using MarketCore.Utills;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using MarketCore.Data;
using static MarketCore.API.MarketAPI.Models.UserModel;

namespace MarketCore.API
{
    public class SteamAPI
    {
        public static async Task<string> GetAvatarUrlAsync()
        {
            try
            {
                string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={Config.Steam_API_Key}&steamids={Config.SteamId32}";

                User user_Date = JsonConvert.DeserializeObject<User>(await HttpUtils.GetResponseAsync(actionUrl));

                return user_Date.Response.Players.First().Avatarfull;
            }
            catch { return string.Empty; }

        }
        public static BitmapImage GetImage(string item_name, string? wear = null)
        {
            string Image_Url;

            if (wear == null)
                Image_Url = $"https://cdn.csgo.com//item/{item_name}/300.png";
            else
                Image_Url = $"https://cdn.csgo.com//item/{item_name} ({wear})/300.png";

            var bitmapImage = new BitmapImage
            {
                UriSource = new Uri(Image_Url)
            };

            return bitmapImage;
        }
        public static async Task<string> GetNicknameAsync()
        {
            try
            {
                string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={Config.Steam_API_Key}&steamids={Config.SteamId32}";

                User user_Date = JsonConvert.DeserializeObject<User>(await HttpUtils.GetResponseAsync(actionUrl));
                return user_Date.Response.Players.First().Personaname;
            }
            catch { return string.Empty; }
        }
    }
}
