using System;
using System.Collections.Generic;

namespace MarketBot.Date
{
    class User_Date
    {
        private static string? market_currency = string.Empty;
        private static string? telegram_User_Id = string.Empty;
        private static string? steam_API_Key = string.Empty;
        private static string? stemaId32 = string.Empty;
        private static string? market_API_Key = string.Empty;
        private static string? current_item = string.Empty;
        private static string? current_sell_item = string.Empty;
        private static bool tradeStatus = false;

        public static string? Current_sell_item { get => current_sell_item; set => current_sell_item = value; }
        public static string? Current_item { get => current_item; set => current_item = value; }
        public static string? Market_API_Key { get => market_API_Key; set => market_API_Key = value; }
        public static string? StemaId32 { get => stemaId32; set => stemaId32 = value; }
        public static string? Steam_API_Key { get => steam_API_Key; set => steam_API_Key = value; }
        public static string? Telegram_User_Id { get => telegram_User_Id; set => telegram_User_Id = value; }
        public static string? Market_currency { get => market_currency; set => market_currency = value; }
        public static bool TradeStatus { get => tradeStatus; set => tradeStatus = value; }

        public struct Balans
        {
            public double money { get; set; }
            public string currency { get; set; }
            public bool success { get; set; }
        }

        public struct Player
        {
            public string steamid { get; set; }
            public int communityvisibilitystate { get; set; }
            public int profilestate { get; set; }
            public string personaname { get; set; }
            public int commentpermission { get; set; }
            public string profileurl { get; set; }
            public string avatar { get; set; }
            public string avatarmedium { get; set; }
            public string avatarfull { get; set; }
            public string avatarhash { get; set; }
            public int lastlogoff { get; set; }
            public int personastate { get; set; }
            public string primaryclanid { get; set; }
            public int timecreated { get; set; }
            public int personastateflags { get; set; }
        }

        public struct Response
        {
            public IList<Player> players { get; set; }
        }

        public struct User
        {
            public Response response { get; set; }
        }
        public struct Ping
        {
            public bool success { get; set; }
            public string ping { get; set; }
            public bool online { get; set; }
            public bool p2p { get; set; }
            public bool steamApiKey { get; set; }
        }
        public struct Item
        {
            public string item_id { get; set; }
            public string assetid { get; set; }
            public string classid { get; set; }
            public string instanceid { get; set; }
            public string real_instance { get; set; }
            public string market_hash_name { get; set; }
            public int position { get; set; }
            public string price { get; set; }
            public string currency { get; set; }
            public string status { get; set; }
            public int live_time { get; set; }
            public object left { get; set; }
            public string botid { get; set; }
        }

        public struct Items
        {
            public bool success { get; set; }
            public IList<Item> items { get; set; }
        }
        public struct Item_Steam
        {
            public string id { get; set; }
            public string classid { get; set; }
            public string instanceid { get; set; }
            public string market_hash_name { get; set; }
            public double market_price { get; set; }
            public int tradable { get; set; }
        }

        public struct Inventory
        {
            public bool success { get; set; }
            public IList<Item_Steam> items { get; set; }
        }
        public struct Sell
        {
            public bool success { get; set; }
            public string item_id { get; set; }
        }
        public struct Update
        {
            public bool success { get; set; }
            public string error { get; set; }
        }
        public struct Data
        {
            public string market_hash_name { get; set; }
            private string price_;
            public string price
            {
                get
                {
                    if (price_.Length <= 2)
                        return "0," + price_ + " " + Market_currency; // its so bad :(
                    else
                        return price_.Insert(price_.Length - 2, ",") + " " + Market_currency;
                }
                set { price_ = value; }
            }
            public string class_ { get; set; }
            public int instance { get; set; }
            public int count { get; set; }
        }

        public struct MarketPrice
        {
            public bool success { get; set; }
            public string currency { get; set; }
            public IList<Data> data { get; set; }
        }
        public struct Datum
        {
            private string time_;
            public string time
            {
                get
                {
                    return time = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(time_)).DateTime.ToString(); ;
                }
                set { time_ = value; }
            }

            public string event_ { get; set; }
            public string item_id { get; set; }
            public string market_hash_name { get; set; }
            public string class_ { get; set; }
            public string instance { get; set; }
            public string paid { get; set; }
            public string currency { get; set; }
            private string stage_;
            public string stage
            {
                get
                {
                    if (stage_ == "1")
                        return "Buy";
                    else if (stage_ == "2")
                        return "Sell";
                    else
                        return "Canceled";
                }
                set { stage_ = value; }
            }
            public object for_ { get; set; }
            public string custom_id { get; set; }
            public int app { get; set; }

            private string price_;
            public string price
            {
                get
                {
                    if (price_ == null)
                    {
                        if (paid.Length <= 2)
                            return "0," + paid + " " + currency;
                        else
                            return paid.Insert(paid.Length - 2, ",") + " " + Market_currency;
                    }

                    else if (price_.Length <= 2)
                        return "0," + price_ + " " + currency;

                    else
                        return price_.Insert(price_.Length - 2, ",") + " " + Market_currency;
                }
                set { price_ = value; }
            }
            public string received { get; set; }
            public string id { get; set; }
            public string amount { get; set; }
            public string status { get; set; }
            public string imageUrl
            {
                get
                {
                    return $"https://cdn.csgo.com//item/{market_hash_name}/300.png";
                }
            }
        }

        public struct MarketHistory
        {
            public bool success { get; set; }
            public IList<Datum> data { get; set; }
        }
        public struct TradeRequestGive
        {
            public bool success { get; set; }
            public string nick { get; set; }
            public string bot_id { get; set; }
            public string profile { get; set; }
            public string secret { get; set; }
            public IList<object> items { get; set; }
            public string trade { get; set; }
        }
        public struct Order
        {
            public string hash_name { get; set; }
            public string count { get; set; }
            public string date { get; set; }
            private string price_;
            public string price
            {
                get
                {
                    if (price_.Length <= 2)
                        return "0," + price_ + " " + Market_currency; // its so bad :(
                    else
                        return price_.Insert(price_.Length - 2, ",") + " " + Market_currency;
                }
                set { price_ = value; }
            }
            public string currency { get; set; }
            public object partner { get; set; }
            public object token { get; set; }
            public string imageUrl
            {
                get
                {
                    return $"https://cdn.csgo.com//item/{hash_name}/300.png";
                }
            }
        }

        public struct OrdersDate
        {
            public bool success { get; set; }
            public IList<Order> orders { get; set; }
        }
    }
}
