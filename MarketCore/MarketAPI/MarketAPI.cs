using Newtonsoft.Json;
using static MarketCore.MarketAPI.Models.MarketModel;
using static MarketCore.MarketAPI.Models.OrdersModel;
using static MarketCore.MarketAPI.Models.UserModel;

namespace MarketCore.MarketAPI
{
    public sealed class MarketAPI
    {
        private static MarketAPI _instance;
        private string Market_API_Key;

        private MarketAPI(string apiKey)
        {
            Market_API_Key = apiKey;
        }

        public static MarketAPI Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("MarketAPI has not been initialized");
                }
                return _instance;
            }
        }

        public static void Initialize(string apiKey)
        {
            if (_instance == null)
            {
                _instance = new MarketAPI(apiKey);
            }
            else
            {
                throw new InvalidOperationException("MarketAPI has already been initialized");
            }
        }

        public void SetApiKey(string apiKey)
        {
            Market_API_Key = apiKey;
        }

        /// <summary>
        /// Receive a request from the server
        /// </summary>
        public static async Task<string> GetResponseAsync(string url)
        {
            string responseBody = string.Empty;
            try
            {
                using var client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                using HttpContent content = response.Content;
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch
            {

            }
            return responseBody;
        }

        /// <summary>
        /// Request inventory cash update (it is recommended to do after each accepted trade offer).
        /// </summary>
        public async void UpdateInventoryAsync()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/update-inventory/?key={Market_API_Key}";

            await GetResponseAsync(actionUrl);
        }

        /// <summary>
        /// Get the amount on the balance and current currency.
        /// </summary>
        public async Task<string> GetMoneyAsync()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/get-money?key={Market_API_Key}";
            try
            {
                Balans user_Date = JsonConvert.DeserializeObject<Balans>(await GetResponseAsync(actionUrl));
                Market_currency = user_Date.Currency;
                return user_Date.Money.ToString() + " " + Market_currency;
            }
            catch { return ""; }
        }

        /// <summary>
        /// Enable sales on https://market.csgo.com/sell/
        /// </summary>
        public async Task<bool> GetPingAsync()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/ping?key={Market_API_Key}";
            try
            {
                PingInfo user_Date = JsonConvert.DeserializeObject<PingInfo>(await GetResponseAsync(actionUrl));
                return user_Date.Online;
            }
            catch { return false; }
        }

        /// <summary>
        /// Get all your items on the market.
        /// </summary>
        public async Task<ItemList> GetItemsAsync()
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/items?key={Market_API_Key}";

                ItemList user_Date = JsonConvert.DeserializeObject<ItemList>(await GetResponseAsync(actionUrl));
                return user_Date;
            }
            catch { return new ItemList(); }
        }

        /// <summary>
        /// Get all your items from Steam inventory.
        /// </summary>
        public async Task<Inventory> GetSteamInventoryAsync()
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/my-inventory/?key={Market_API_Key}";

                Inventory user_Date = JsonConvert.DeserializeObject<Inventory>(await GetResponseAsync(actionUrl));
                return user_Date;
            }
            catch { return new Inventory(); }
        }

        /// <summary>
        /// Option to request a single item
        /// </summary>
        /// <param name="item_name">Item name, it can be taken from the Steam inventory.</param>
        public async Task<MarketPrice> GetMarketPriceAsync(string item_name)
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/search-item-by-hash-name?key={Market_API_Key}&hash_name={item_name}";

                MarketPrice user_Date = JsonConvert.DeserializeObject<MarketPrice>(await GetResponseAsync(actionUrl));
                return user_Date;
            }
            catch { return new MarketPrice(); }
        }

        /// <summary>
        /// Getting a history of purchases and sales at all sites
        /// </summary>
        public async Task<MarketHistory> GetMarketHistoryAsync()
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/operation-history?key={Market_API_Key}&date={DateTimeOffset.Now.Add(TimeSpan.FromDays(-90)).ToUnixTimeSeconds()}&date_end={DateTimeOffset.Now.ToUnixTimeSeconds()}";

                MarketHistory user_history = JsonConvert.DeserializeObject<MarketHistory>(await GetResponseAsync(actionUrl));
                return user_history;
            }
            catch { return new MarketHistory(); }
        }

        /// <summary>
        /// Set item for sale. To get a list of items for selling, use the method <see cref="GetSteamInventoryAsync"/>.
        /// </summary>
        ///  <param name="item_id">The ID of the item in Market system</param>
        ///  <param name="currency">The currency (RUB, USD, EUR) an additional check, if specified is not equal to the current set the purchase will be cancelled.</param>
        ///  <param name="price">If you specify 0, the item will be removed from sale</param>
        public async Task<Sell> SetSellAsync(string item_id, string price, string currency)
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/add-to-sale?key={Market_API_Key}&id={item_id}&price={price}00&cur={currency}";

                Sell user_Date = JsonConvert.DeserializeObject<Sell>(await GetResponseAsync(actionUrl));
                return user_Date;
            }
            catch { return new Sell(); }
        }

        /// <summary>
        /// Set a new price on the item, or remove from sale.
        /// </summary>
        ///  <param name="item_id">The ID of the item in Market system</param>
        ///   <param name="currency">The currency (RUB, USD, EUR) an additional check, if specified is not equal to the current set the purchase will be cancelled.</param>
        ///    <param name="price">If you specify 0, the item will be removed from sale</param>
        public async Task<Update> SetPriceAsync(string item_id, string price, string currency)
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/set-price?key={Market_API_Key}&item_id={item_id}&price={price}00&cur={currency}";

                Update user_Date = JsonConvert.DeserializeObject<Update>(await GetResponseAsync(actionUrl));
                return user_Date;
            }
            catch { return new Update(); }
        }

        /// <summary>
        /// Create a request for the transfer of purchased items.
        /// </summary>
        public async Task<bool> GetTradeRequesTakeAsync()
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/trade-request-take?key={Market_API_Key}";

                TradeRequestGive tradeRequestTake = JsonConvert.DeserializeObject<TradeRequestGive>(await GetResponseAsync(actionUrl));
                return tradeRequestTake.Success;
            }
            catch { return false; }
        }

        /// <summary>
        /// Request data to transfer the item to the buyer
        /// </summary>
        public async Task<bool> GetTradeRequestGiveAsync()
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/trade-request-give-p2p?key={Market_API_Key}";

                TradeRequestGive tradeRequestGive = JsonConvert.DeserializeObject<TradeRequestGive>(await GetResponseAsync(actionUrl));
                return tradeRequestGive.Success;
            }
            catch { return false; }
        }

        /// <summary>
        /// Getting a list of your orders
        /// </summary>
        public async Task<OrdersList> GetOrdersAsync()
        {
            string actionUrl = $"https://market.csgo.com/api/v2/get-orders?key={Market_API_Key}&page=0";
            var ordersRequestGive = new OrdersList();

            try
            {
                ordersRequestGive = JsonConvert.DeserializeObject<OrdersList>(await GetResponseAsync(actionUrl));

                for (int i = 1; i < 9999; i++)
                {
                    actionUrl = $"https://market.csgo.com/api/v2/get-orders?key={Market_API_Key}&page={i}";

                    var request = JsonConvert.DeserializeObject<OrdersList>(await GetResponseAsync(actionUrl));

                    if (request.Orders.Count == 0)
                        break;

                    ordersRequestGive.Orders = ordersRequestGive.Orders.Concat(request.Orders).ToList();
                }
            }
            catch { }

            return ordersRequestGive;
        }

        /// <summary>
        /// Adding, modifying and deleting an order
        /// </summary>
        ///  <param name="market_hash_name">Item identifiers.</param>
        ///  <param name="wear">The wear level of the item</param>
        ///  <param name="count">Number of items to buy</param>
        ///  <param name="price">If you specify 0, the order will be removed from sale</param>
        public async Task<OrdersList> SetOrderAsync(string market_hash_name, string wear, string count, string price)
        {
            string actionUrl = $"https://market.csgo.com/api/v2/set-order?key={Market_API_Key}&market_hash_name={market_hash_name} {wear}&count={count}&price={price}";

            var ordersRequestGive = JsonConvert.DeserializeObject<OrdersList>(await GetResponseAsync(actionUrl));
            return ordersRequestGive;
        }

        /// <summary>
        /// Getting a history of executed orders.
        /// </summary>
        public async Task<OrdersLog> GetOrdersLogAsync()
        {
            try
            {
                string actionUrl = $"https://market.csgo.com/api/v2/get-orders-log?key={Market_API_Key}&page=1";

                var ordersRequestGive = JsonConvert.DeserializeObject<OrdersLog>(await GetResponseAsync(actionUrl));
                return ordersRequestGive;
            }
            catch { return new OrdersLog(); }
        }
    }

}