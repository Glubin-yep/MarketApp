using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Media.Imaging;
using static MarketBot.User_Date;

namespace MarketBot
{
    class HttpGetInfo
    {
        private static string market_API_key = string.Empty;
        private static string stemaId32 = string.Empty;
        private static string steam_API_key = string.Empty;
        public static void ReadConfig()
        {
            using (StreamReader reader = new StreamReader("Config.txt"))
            {
                string line;
                int i = 1;
                while ((line = reader.ReadLine()) != null)
                {


                    if (i == 1)
                    {
                        market_API_key = line.Split(": ")[1];
                        i++;
                    }
                    else if (i == 2)
                    {
                        stemaId32 = line.Split(": ")[1];
                        i++;
                    }
                    else if (i == 3)
                    {
                        steam_API_key = line.Split(": ")[1];
                    }
                }
            }
        }
        private static string GetAPI(string url)
        {
            string responseData = string.Empty;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            using (HttpClient client = new HttpClient())
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseData = reader.ReadToEnd();
                        reader.Close();
                    }
                }
            }
            return responseData;
        }
        public static string GetMoney()
        {
            var user_Date = new User_Date.Balans();
            string actionUrl = $"https://market.csgo.com/api/v2/get-money?key={market_API_key}";


            user_Date = JsonConvert.DeserializeObject<User_Date.Balans>(GetAPI(actionUrl));
            return user_Date.money.ToString();
        }
        public static BitmapImage GetAvatar()
        {
            var user_Date = new User_Date.User();
            string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={steam_API_key}&steamids={stemaId32}";

            user_Date = JsonConvert.DeserializeObject<User_Date.User>(GetAPI(actionUrl));

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri($"{user_Date.response.players.First().avatarfull}"); ;
            bitmapImage.EndInit();


            return bitmapImage;
        }
        public static BitmapImage GetImage(string item_name)
        {
            string Image_Url = $"https://cdn.csgo.com//item/{Get_Id_Name(item_name, "name")}/150.png";

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(Image_Url);
            bitmapImage.EndInit();

            return bitmapImage;
        }
        public static string GetNickname()
        {
            var user_Date = new User_Date.User();
            string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={steam_API_key}&steamids={stemaId32}";

            user_Date = JsonConvert.DeserializeObject<User_Date.User>(GetAPI(actionUrl));

            return user_Date.response.players.First().personaname;
        }
        public static bool GetPing()
        {
            var user_Date = new User_Date.Ping();
            string actionUrl = $"https://market.csgo.com/api/v2/ping?key={market_API_key}";

            user_Date = JsonConvert.DeserializeObject<User_Date.Ping>(GetAPI(actionUrl));
            return user_Date.online;
        }
        public static Items GetItems()
        {
            var user_Date = new User_Date.Items();
            string actionUrl = $"https://market.csgo.com/api/v2/items?key={market_API_key}";

            user_Date = JsonConvert.DeserializeObject<User_Date.Items>(GetAPI(actionUrl));
            return user_Date;
        }
        public static Inventory GetSteamInventory()
        {
            var user_Date = new User_Date.Inventory();
            string actionUrl = $"https://market.csgo.com/api/v2/my-inventory/?key={market_API_key}";

            user_Date = JsonConvert.DeserializeObject<User_Date.Inventory>(GetAPI(actionUrl));
            return user_Date;
        }
        public static string Get_Id_Name(string current_item, string mode)
        {
            string[] strings = current_item.Split(" /");
            if (mode == "name")
                return strings[0];

            return strings[strings.Length - 1];
        }
        public static MarketPrice GetMarketPrice(string item_name)
        {
            var user_Date = new User_Date.MarketPrice();
            item_name = Get_Id_Name(item_name, "name");
            string actionUrl = $"https://market.csgo.com/api/v2/search-item-by-hash-name?key={market_API_key}&hash_name={item_name}";

            user_Date = JsonConvert.DeserializeObject<User_Date.MarketPrice>(GetAPI(actionUrl));
            return user_Date;
        }
        public static Sell SetSell(string id, string price, string currency)
        {
            var user_Date = new User_Date.Sell();
            id = Get_Id_Name(id, "id");
            string actionUrl = $"https://market.csgo.com/api/v2/add-to-sale?key={market_API_key}&id={id}&price={price}00&cur={currency}";

            user_Date = JsonConvert.DeserializeObject<User_Date.Sell>(GetAPI(actionUrl));
            return user_Date;
        }
        public static Update SetPrice(string item_id, string price, string currency)
        {
            var user_Date = new User_Date.Update();
            item_id = Get_Id_Name(item_id, "id");
            string actionUrl = $"https://market.csgo.com/api/v2/set-price?key={market_API_key}&item_id={item_id}&price={price}00&cur={currency}";

            user_Date = JsonConvert.DeserializeObject<User_Date.Update>(GetAPI(actionUrl));
            return user_Date;
        }

    }
}
