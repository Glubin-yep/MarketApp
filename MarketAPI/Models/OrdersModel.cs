namespace MarketLIB.Models
{
    public class OrdersModel
    {
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
                        return "0," + price_ + " " + MarketModel.Market_currency; // its so bad :(
                    else
                        return price_.Insert(price_.Length - 2, ",") + " " + MarketModel.Market_currency;
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

        public struct OrdersList
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
