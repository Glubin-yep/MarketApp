using MarketApp.Date;
using MarketBot.Parsing;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static MarketApp.Date.OrdersModel;
using static MarketApp.Date.UserModel;
using static MarketBot.Date.MarketModel;

namespace MarketBot.API
{
    class MarketAPI
    {
        public static async Task<string> GetResponseAsync(string url)
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
        public static async void UpdateInventoryAsync()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/update-inventory/?key={Config.Market_API_Key}";

            await GetResponseAsync(actionUrl);
        }
        public static async Task<string> GetMoneyAsync()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/get-money?key={Config.Market_API_Key}";
            try
            {

                Balans user_Date = JsonConvert.DeserializeObject<Balans>(await GetResponseAsync(actionUrl));
                Market_currency = user_Date.Currency;
                return user_Date.Money.ToString();
            }
            catch { return ""; }
        }
        public static async Task<bool> GetPingAsync()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/ping?key={Config.Market_API_Key}";
            try
            {
                PingInfo user_Date = JsonConvert.DeserializeObject<PingInfo>(await GetResponseAsync(actionUrl));
                return user_Date.Online;
            }
            catch { return false; }
        }
        public static async Task<ItemList> GetItemsAsync()
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/items?key={Config.Market_API_Key}";

                ItemList user_Date = JsonConvert.DeserializeObject<ItemList>(await GetResponseAsync(actionUrl));
                return user_Date;
            }
            catch { return new ItemList(); }
        }
        public static async Task<Inventory> GetSteamInventoryAsync()
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/my-inventory/?key={Config.Market_API_Key}";

                Inventory user_Date = JsonConvert.DeserializeObject<Inventory>(await GetResponseAsync(actionUrl));
                return user_Date;
            }
            catch { return new Inventory(); }
        }
        public static async Task<MarketPrice> GetMarketPriceAsync(string item_name)
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/search-item-by-hash-name?key={Config.Market_API_Key}&hash_name={item_name}";

                MarketPrice user_Date = JsonConvert.DeserializeObject<MarketPrice>(await GetResponseAsync(actionUrl));
                return user_Date;
            }
            catch { return new MarketPrice(); }
        }
        public static async Task<MarketHistory> GetMarketHistoryAsync()
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/operation-history?key={Config.Market_API_Key}&date={DateTimeOffset.Now.Add(TimeSpan.FromDays(-90)).ToUnixTimeSeconds()}&date_end={DateTimeOffset.Now.ToUnixTimeSeconds()}";

                MarketHistory user_history = JsonConvert.DeserializeObject<MarketHistory>(await GetResponseAsync(actionUrl));
                return user_history;
            }
            catch { return new MarketHistory(); }
        }
        public static async Task<Sell> SetSellAsync(string item_id, string price, string currency)
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/add-to-sale?key={Config.Market_API_Key}&id={item_id}&price={price}00&cur={currency}";

                Sell user_Date = JsonConvert.DeserializeObject<Sell>(await GetResponseAsync(actionUrl));
                return user_Date;
            }
            catch { return new Sell(); }
        }
        public static async Task<Update> SetPriceAsync(string item_id, string price, string currency)
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/set-price?key={Config.Market_API_Key}&item_id={item_id}&price={price}00&cur={currency}";

                Update user_Date = JsonConvert.DeserializeObject<Update>(await GetResponseAsync(actionUrl));
                return user_Date;
            }
            catch { return new Update(); }
        }
        public static async Task<bool> GetTradeRequesTakeAsync()
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/trade-request-take?key={Config.Market_API_Key}";

                TradeRequestGive tradeRequestTake = JsonConvert.DeserializeObject<TradeRequestGive>(await GetResponseAsync(actionUrl));
                return tradeRequestTake.Success;
            }
            catch { return false; }
        }
        public static async Task<bool> GetTradeRequestGiveAsync()
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/trade-request-give-p2p?key={Config.Market_API_Key}";

                TradeRequestGive tradeRequestGive = JsonConvert.DeserializeObject<TradeRequestGive>(await GetResponseAsync(actionUrl));
                return tradeRequestGive.Success;
            }
            catch { return false; }
        }
        public static async Task<OrdersList> GetOrdersAsync()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/get-orders?key={Config.Market_API_Key}&page=0";

            var ordersRequestGive = JsonConvert.DeserializeObject<OrdersList>(await GetResponseAsync(actionUrl));
            return ordersRequestGive;
        }
        public static async Task<OrdersList> SetOrderAsync(string market_hash_name, string wear, string count, string price) // if price == 0 order deleted
        {
            string actionUrl = $"https://market.csgo.com/api/v2/set-order?key={Config.Market_API_Key}&market_hash_name={market_hash_name} {wear}&count={count}&price={price}";

            var ordersRequestGive = JsonConvert.DeserializeObject<OrdersList>(await GetResponseAsync(actionUrl));
            return ordersRequestGive;
        }
        public static async Task<OrdersLog> GetOrdersLogAsync()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/get-orders-log?key={Config.Market_API_Key}&page=0";

            var ordersRequestGive = JsonConvert.DeserializeObject<OrdersLog>(await GetResponseAsync(actionUrl));
            return ordersRequestGive;
        }

    }
}