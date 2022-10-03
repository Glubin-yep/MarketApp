using System;
using System.Collections.Generic;

namespace MarketBot.Date
{
    class User_Date
    {
        public static string Current_sell_item { get; set; } = string.Empty; 
        public static string Current_item { get; set; } = string.Empty;
        public static string Market_currency { get; set; } = string.Empty;

        public struct Balans
        {
            public double Money { get; set; }
            public string Currency { get; set; }
            public bool Success { get; set; }
        }

        public struct Player
        {
            public string Personaname { get; set; }
            public string Avatarfull { get; set; }
        }

        public struct Response
        {
            public IList<Player> Players { get; set; }
        }

        public struct User
        {
            public Response Response { get; set; }
        }
        public struct PingInfo
        {
            public bool Success { get; set; }
            public string Ping { get; set; }
            public bool Online { get; set; }
            public bool P2P { get; set; }
            public bool SteamApiKey { get; set; }
        }
        public struct Item
        {
            public string Item_id { get; set; }
            public string Assetid { get; set; }
            public string Classid { get; set; }
            public string Instanceid { get; set; }
            public string Real_instance { get; set; }
            public string Market_hash_name { get; set; }
            public int Position { get; set; }
            public string Price { get; set; }
            public string Currency { get; set; }
            public string Status { get; set; }
            public int Live_time { get; set; }
            public object Left { get; set; }
            public string Botid { get; set; }
            public string ImageUrl
            {
                get
                {
                    return $"https://cdn.csgo.com//item/{Market_hash_name}/300.png";
                }
            }
        }

        public struct ItemList
        {
            public bool Success { get; set; }
            public IList<Item> Items { get; set; }
        }
        public struct Item_Steam
        {
            public string Id { get; set; }
            public string Classid { get; set; }
            public string Instanceid { get; set; }
            public string Market_hash_name { get; set; }
            public double Market_price { get; set; }
            public int Tradable { get; set; }
        }

        public struct Inventory
        {
            public bool Success { get; set; }
            public IList<Item_Steam> Items { get; set; }
        }
        public struct Sell
        {
            public bool Success { get; set; }
            public string Item_id { get; set; }
        }
        public struct Update
        {
            public bool Success { get; set; }
            public string Error { get; set; }
        }
        public struct Data
        {
            public string Market_hash_name { get; set; }
            private string price_;
            public string Price
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
            public string Class_ { get; set; }
            public int Instance { get; set; }
            public int Count { get; set; }
        }

        public struct MarketPrice
        {
            public bool Success { get; set; }
            public string Currency { get; set; }
            public IList<Data> Data { get; set; }
        }
        public struct Datum
        {
            private string time_;
            public string Time
            {
                get
                {
                    return Time = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(time_)).DateTime.ToString(); ;
                }
                set { time_ = value; }
            }

            public string Event_ { get; set; }
            public string Item_id { get; set; }
            public string Market_hash_name { get; set; }
            public string Class_ { get; set; }
            public string Instance { get; set; }
            public string Paid { get; set; }
            public string Currency { get; set; }
            private string stage_;
            public string Stage
            {
                get
                {
                    if (Paid != null && stage_ != "5")
                        return "Buy";
                    else if (stage_ == "2")
                        return "Sell";
                    else
                        return "Canceled";
                }
                set { stage_ = value; }
            }
            public object For_ { get; set; }
            public string Custom_id { get; set; }
            public int App { get; set; }

            private string price_;
            public string Price
            {
                get
                {
                    if (price_ == null)
                    {
                        if (Paid.Length <= 2)
                            return "0," + Paid + " " + Market_currency;
                        else
                            return Paid.Insert(Paid.Length - 2, ",") + " " + Market_currency;
                    }

                    else if (price_.Length <= 2)
                        return "0," + price_ + " " + Market_currency;

                    else
                        return price_.Insert(price_.Length - 2, ",") + " " + Market_currency;
                }
                set
                {
                    int temp = Int32.Parse(value);
                    temp -= (temp * 5 / 100);
                    price_ = temp.ToString();
                }
            }
            public string Received { get; set; }
            public string Id { get; set; }
            public string Amount { get; set; }
            public string Status { get; set; }
            public string ImageUrl
            {
                get
                {
                    return $"https://cdn.csgo.com//item/{Market_hash_name}/300.png";
                }
            }
        }

        public struct MarketHistory
        {
            public bool Success { get; set; }
            public IList<Datum> Data { get; set; }
        }
        public struct TradeRequestGive
        {
            public bool Success { get; set; }
            public string Nick { get; set; }
            public string Bot_id { get; set; }
            public string Profile { get; set; }
            public string Secret { get; set; }
            public IList<object> Items { get; set; }
            public string Trade { get; set; }
        }
        public struct Order
        {
            public string Hash_name { get; set; }
            public string Count { get; set; }
            public string Date { get; set; }
            private string price_;
            public string Price
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
            public string Currency { get; set; }
            public object Partner { get; set; }
            public object Token { get; set; }
            public string ImageUrl
            {
                get
                {
                    return $"https://cdn.csgo.com//item/{Hash_name}/300.png";
                }
            }
        }

        public struct OrdersDate
        {
            public bool Success { get; set; }
            public IList<Order> Orders { get; set; }
        }
        public struct Orderlog
        {
            public string Hash_name { get; set; }
            public int Item_id { get; set; }
            public string Created { get; set; }
            public string Executed { get; set; }
            public int Price { get; set; }
            public string Currency { get; set; }
        }

        public struct OrdersLog
        {
            public bool Success { get; set; }
            public IList<Orderlog> Orders { get; set; }
        }
    }
}
