using MarketBot.Parsing;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static MarketBot.Date.User_Date;

namespace MarketBot.API
{
    class MarketAPI
    {
        public static async Task<string> GetAPI(string url)
        {
            string responseBody = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    using (HttpContent content = response.Content)
                    {
                        responseBody = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch
            {

            }
            return responseBody;
        }
        public static async void UpdateInventory()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/update-inventory/?key={Market_API_Key}";

            await GetAPI(actionUrl);
        }
        public static async Task<string> GetMoney()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/get-money?key={Market_API_Key}";

            Balans user_Date = JsonConvert.DeserializeObject<Balans>(await GetAPI(actionUrl));
            Market_currency = user_Date.currency;
            return user_Date.money.ToString();
        }
        public static async Task<bool> GetPing()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/ping?key={Market_API_Key}";

            Ping user_Date = JsonConvert.DeserializeObject<Ping>(await GetAPI(actionUrl));
            return user_Date.online;
        }
        public static async Task<Items> GetItems()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/items?key={Market_API_Key}";

            Items user_Date = JsonConvert.DeserializeObject<Items>(await GetAPI(actionUrl));
            return user_Date;
        }
        public static async Task<Inventory> GetSteamInventory()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/my-inventory/?key={Market_API_Key}";

            Inventory user_Date = JsonConvert.DeserializeObject<Inventory>(await GetAPI(actionUrl));
            return user_Date;
        }
        public static async Task<MarketPrice> GetMarketPrice(string item_name)
        {
            item_name = DateParsing.Get_Id_Name(item_name, "name");
            string actionUrl = $"https://market.csgo.com/api/v2/search-item-by-hash-name?key={Market_API_Key}&hash_name={item_name}";

            MarketPrice user_Date = JsonConvert.DeserializeObject<MarketPrice>(await GetAPI(actionUrl));
            return user_Date;
        }
        public static async Task<MarketHistory> GetMarketHistory()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/operation-history?key={Market_API_Key}&date={DateTimeOffset.Now.Add(TimeSpan.FromDays(-90)).ToUnixTimeSeconds()}&date_end={DateTimeOffset.Now.ToUnixTimeSeconds()}";

            MarketHistory user_history = JsonConvert.DeserializeObject<MarketHistory>(await GetAPI(actionUrl));
            return user_history;
        }
        public static async Task<Sell> SetSell(string id, string price, string currency)
        {
            id = DateParsing.Get_Id_Name(id, "id");
            string actionUrl = $"https://market.csgo.com/api/v2/add-to-sale?key={Market_API_Key}&id={id}&price={price}00&cur={currency}";

            Sell user_Date = JsonConvert.DeserializeObject<Sell>(await GetAPI(actionUrl));
            return user_Date;
        }
        public static async Task<Update> SetPrice(string item_id, string price, string currency)
        {
            item_id = DateParsing.Get_Id_Name(item_id, "id");
            string actionUrl = $"https://market.csgo.com/api/v2/set-price?key={Market_API_Key}&item_id={item_id}&price={price}00&cur={currency}";

            Update user_Date = JsonConvert.DeserializeObject<Update>(await GetAPI(actionUrl));
            return user_Date;
        }
        public static async Task<bool> TradeRequesTake()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/trade-request-take?key={Market_API_Key}";

            TradeRequestGive tradeRequestTake = JsonConvert.DeserializeObject<TradeRequestGive>(await GetAPI(actionUrl));
            return tradeRequestTake.success;
        }
        public static async Task<bool> TradeRequestGive()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/trade-request-give-p2p?key={Market_API_Key}";

            TradeRequestGive tradeRequestGive = JsonConvert.DeserializeObject<TradeRequestGive>(await GetAPI(actionUrl));
            return tradeRequestGive.success;
        }
        public static async Task<OrdersDate> GetOrders()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/get-orders?key={Market_API_Key}&page=0";

            var ordersRequestGive = JsonConvert.DeserializeObject<OrdersDate>(await GetAPI(actionUrl));
            return ordersRequestGive;
        }
        public static async Task<OrdersDate> SetOrder(string market_hash_name, string wear, string count, string price) // if price == 0 order deleted
        {
            string actionUrl = $"https://market.csgo.com/api/v2/set-order?key={Market_API_Key}&market_hash_name={market_hash_name} {wear}&count={count}&price={price}";

            var ordersRequestGive = JsonConvert.DeserializeObject<OrdersDate>(await GetAPI(actionUrl));
            return ordersRequestGive;
        }
        public static async Task<OrdersLog> GetOrdersLog()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/get-orders-log?key={Market_API_Key}&page=0";

            var ordersRequestGive = JsonConvert.DeserializeObject<OrdersLog>(await GetAPI(actionUrl));
            return ordersRequestGive;
        }
        
    }
}
