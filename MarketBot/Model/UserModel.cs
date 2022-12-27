using System.Collections.Generic;

namespace MarketApp.Date
{
    class UserModel
    {
        public class Balans
        {
            public  double Money { get; set; }
            public  string Currency { get; set; }
            public  bool Success { get; set; }
        }

        public class Player
        {
            public  string Personaname { get; set; }
            public  string Avatarfull { get; set; }
        }

        public class Response
        {
            public  IList<Player> Players { get; set; }
        }

        public class User
        {
            public  Response Response { get; set; }
        }
        public class PingInfo
        {
            public bool Success { get; set; }
            public string Ping { get; set; }
            public bool Online { get; set; }
            public bool P2P { get; set; }
            public bool SteamApiKey { get; set; }
        }
    }
}
