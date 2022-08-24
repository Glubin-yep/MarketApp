using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Media.Imaging;
using static MarketBot.Date.User_Date;

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
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var  reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseData = reader.ReadToEnd();
                        reader.Close();
                    }
                }
            }
            catch
            {

            }

            return responseData;
        }
        public static string GetMoney()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/get-money?key={market_API_key}";

            Balans user_Date = JsonConvert.DeserializeObject<Balans>(GetAPI(actionUrl));
            return user_Date.money.ToString() + " " + user_Date.currency;
        }
        public static BitmapImage GetAvatar()
        {
            string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={steam_API_key}&steamids={stemaId32}";

            User user_Date = JsonConvert.DeserializeObject<User>(GetAPI(actionUrl));

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
            string actionUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={steam_API_key}&steamids={stemaId32}";

            User user_Date = JsonConvert.DeserializeObject<User>(GetAPI(actionUrl));

            return user_Date.response.players.First().personaname;
        }
        public static bool GetPing()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/ping?key={market_API_key}";

            Ping user_Date = JsonConvert.DeserializeObject<Ping>(GetAPI(actionUrl));
            return user_Date.online;
        }
        public static Items GetItems()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/items?key={market_API_key}";

            Items user_Date = JsonConvert.DeserializeObject<Items>(GetAPI(actionUrl));
            return user_Date;
        }
        public static Inventory GetSteamInventory()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/my-inventory/?key={market_API_key}";

            Inventory user_Date = JsonConvert.DeserializeObject<Inventory>(GetAPI(actionUrl));
            return user_Date;
        }
        public static string Get_Id_Name(string current_item, string mode)
        {
            string[] strings = current_item.Split(" /");
            if (mode == "name")
                return strings[0];

            return strings[^1];
        }
        public static MarketPrice GetMarketPrice(string item_name)
        {
            item_name = Get_Id_Name(item_name, "name");
            string actionUrl = $"https://market.csgo.com/api/v2/search-item-by-hash-name?key={market_API_key}&hash_name={item_name}";

            MarketPrice user_Date = JsonConvert.DeserializeObject<MarketPrice>(GetAPI(actionUrl));
            return user_Date;
        }
        public static MarketHistory GetMarketHistory()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/operation-history?key={market_API_key}&date={DateTimeOffset.Now.Add(TimeSpan.FromDays(-90)).ToUnixTimeSeconds()}&date_end={DateTimeOffset.Now.ToUnixTimeSeconds()}";

            MarketHistory user_history = JsonConvert.DeserializeObject<MarketHistory>(GetAPI(actionUrl));
            return user_history;
        }
        public static Sell SetSell(string id, string price, string currency)
        {
            id = Get_Id_Name(id, "id");
            string actionUrl = $"https://market.csgo.com/api/v2/add-to-sale?key={market_API_key}&id={id}&price={price}00&cur={currency}";

            Sell user_Date = JsonConvert.DeserializeObject<Sell>(GetAPI(actionUrl));
            return user_Date;
        }
        public static Update SetPrice(string item_id, string price, string currency)
        {
            item_id = Get_Id_Name(item_id, "id");
            string actionUrl = $"https://market.csgo.com/api/v2/set-price?key={market_API_key}&item_id={item_id}&price={price}00&cur={currency}";

            Update user_Date = JsonConvert.DeserializeObject<Update>(GetAPI(actionUrl));
            return user_Date;
        }

        public static bool TradeRequesTake()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/trade-request-take?key={market_API_key}";

            TradeRequestGive tradeRequestTake = JsonConvert.DeserializeObject<TradeRequestGive>(GetAPI(actionUrl));
            return tradeRequestTake.success;
        }
        public static bool TradeRequestGive()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/trade-request-give-p2p?key={market_API_key}";

            TradeRequestGive tradeRequestGive = JsonConvert.DeserializeObject<TradeRequestGive>(GetAPI(actionUrl));
            return tradeRequestGive.success;
        }
    }
}
