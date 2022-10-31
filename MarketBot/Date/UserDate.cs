using System.Collections.Generic;

namespace MarketApp.Date
{
    class UserDate
    {
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
    }
}
