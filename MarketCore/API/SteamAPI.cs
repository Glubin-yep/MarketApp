using MarketCore.Data;
using MarketCore.Utills;
using Newtonsoft.Json;
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
        public static string GetImageUrl(string item_name, string? wear = null)
        {
            string Image_Url;

            if (wear == null)
                Image_Url = $"https://cdn.csgo.com//item/{item_name}/300.png";
            else
                Image_Url = $"https://cdn.csgo.com//item/{item_name} ({wear})/300.png";

            return Image_Url;
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
