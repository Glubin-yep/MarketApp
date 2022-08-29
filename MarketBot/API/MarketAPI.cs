using MarketBot.Parsing;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using static MarketBot.Date.User_Date;

namespace MarketBot.API
{
    class MarketAPI
    {
        public static string GetAPI(string url)
        {
            string responseData = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
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
            string actionUrl = $"https://market.csgo.com/api/v2/get-money?key={Market_API_Key}";

            Balans user_Date = JsonConvert.DeserializeObject<Balans>(GetAPI(actionUrl));
            Market_currency = user_Date.currency;
            return user_Date.money.ToString();
        }
        public static bool GetPing()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/ping?key={Market_API_Key}";

            Ping user_Date = JsonConvert.DeserializeObject<Ping>(GetAPI(actionUrl));
            return user_Date.online;
        }
        public static Items GetItems()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/items?key={Market_API_Key}";

            Items user_Date = JsonConvert.DeserializeObject<Items>(GetAPI(actionUrl));
            return user_Date;
        }
        public static Inventory GetSteamInventory()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/my-inventory/?key={Market_API_Key}";

            Inventory user_Date = JsonConvert.DeserializeObject<Inventory>(GetAPI(actionUrl));
            return user_Date;
        }
        public static MarketPrice GetMarketPrice(string item_name)
        {
            item_name = DateParsing.Get_Id_Name(item_name, "name");
            string actionUrl = $"https://market.csgo.com/api/v2/search-item-by-hash-name?key={Market_API_Key}&hash_name={item_name}";

            MarketPrice user_Date = JsonConvert.DeserializeObject<MarketPrice>(GetAPI(actionUrl));
            return user_Date;
        }
        public static MarketHistory GetMarketHistory()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/operation-history?key={Market_API_Key}&date={DateTimeOffset.Now.Add(TimeSpan.FromDays(-90)).ToUnixTimeSeconds()}&date_end={DateTimeOffset.Now.ToUnixTimeSeconds()}";

            MarketHistory user_history = JsonConvert.DeserializeObject<MarketHistory>(GetAPI(actionUrl));
            return user_history;
        }
        public static Sell SetSell(string id, string price, string currency)
        {
            id = DateParsing.Get_Id_Name(id, "id");
            string actionUrl = $"https://market.csgo.com/api/v2/add-to-sale?key={Market_API_Key}&id={id}&price={price}00&cur={currency}";

            Sell user_Date = JsonConvert.DeserializeObject<Sell>(GetAPI(actionUrl));
            return user_Date;
        }
        public static Update SetPrice(string item_id, string price, string currency)
        {
            item_id = DateParsing.Get_Id_Name(item_id, "id");
            string actionUrl = $"https://market.csgo.com/api/v2/set-price?key={Market_API_Key}&item_id={item_id}&price={price}00&cur={currency}";

            Update user_Date = JsonConvert.DeserializeObject<Update>(GetAPI(actionUrl));
            return user_Date;
        }
        public static bool TradeRequesTake()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/trade-request-take?key={Market_API_Key}";

            TradeRequestGive tradeRequestTake = JsonConvert.DeserializeObject<TradeRequestGive>(GetAPI(actionUrl));
            return tradeRequestTake.success;
        }
        public static bool TradeRequestGive()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/trade-request-give-p2p?key={Market_API_Key}";

            TradeRequestGive tradeRequestGive = JsonConvert.DeserializeObject<TradeRequestGive>(GetAPI(actionUrl));
            return tradeRequestGive.success;
        }
        public static OrdersDate GetOrders()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/get-orders?key={Market_API_Key}&page=0";

            var ordersRequestGive = JsonConvert.DeserializeObject<OrdersDate>(GetAPI(actionUrl));
            return ordersRequestGive;
        }
        public static OrdersDate SetOrder(string market_hash_name, string count, string price) // if price == 0 order deleted
        {
            string actionUrl = $"https://market.csgo.com/api/v2/set-order?key={Market_API_Key}&market_hash_name={market_hash_name}&count={count}&price={price}";

            var ordersRequestGive = JsonConvert.DeserializeObject<OrdersDate>(GetAPI(actionUrl));
            return ordersRequestGive;
        }
        public static OrdersLog GetOrdersLog()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/get-orders-log?key={Market_API_Key}&page=0";

            var ordersRequestGive = JsonConvert.DeserializeObject<OrdersLog>(GetAPI(actionUrl));
            return ordersRequestGive;
        }
    }
}
