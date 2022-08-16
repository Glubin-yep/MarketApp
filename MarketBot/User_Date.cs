﻿using System.Collections.Generic;

namespace MarketBot
{
    public partial class MainWindow
    {
        public class Balans
        {
            public double money { get; set; }
            public string currency { get; set; }
            public bool success { get; set; }
        }

        public class Player
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

        public class Response
        {
            public IList<Player> players { get; set; }
        }

        public class User
        {
            public Response response { get; set; }
        }

    }
}
