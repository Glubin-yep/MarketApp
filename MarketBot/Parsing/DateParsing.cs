﻿using AdonisUI.Controls;
using MarketBot.API;
using MarketBot.Notication;
using System.IO;

namespace MarketBot.Parsing
{
    class DateParsing
    {
        public static void ReadConfig()
        {
            using (var reader = new StreamReader("Config.txt"))
            {
                string line;
                int i = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    if (i == 1)
                    {
                        MarketAPI.Market_API_Key = line.Split(": ")[1];
                        i++;
                    }
                    else if (i == 2)
                    {
                        SteamAPI.StemaId32 = line.Split(": ")[1];
                        i++;
                    }
                    else if (i == 3)
                    {
                        SteamAPI.Steam_API_Key = line.Split(": ")[1];
                        i++;
                    }
                    else if (i == 4)
                    {
                        Notification.Telegram_User_Id = line.Split(": ")[1];
                    }
                }
                if (MarketAPI.Market_API_Key == string.Empty || SteamAPI.StemaId32 == string.Empty || SteamAPI.Steam_API_Key == string.Empty)
                {
                    MessageBox.Show("Entry Steam API or StemaId32 or Market API", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    App.Current.Shutdown();
                }
            }
        }
        public static string Get_Id_Name(string? current_item, string? mode)
        {
            string[] strings = current_item.Split(" /");
            if (mode == "name")
                return strings[0];

            return strings[^1];
        }
    }
}
