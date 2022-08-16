using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBot
{
    class User_Date
    {
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

    }
}
