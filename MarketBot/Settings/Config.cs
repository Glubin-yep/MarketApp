using MarketBot.Notication;
using System;
using System.IO;

namespace MarketApp.Settings
{
    public class Config
    {
        public static string StemaId32 { get; set; } = string.Empty;
        public static string Steam_API_Key { get; set; } = string.Empty;
        public static string Market_API_Key { get; set; } = string.Empty;
        public static string Telegram_User_Id { get; set; } = string.Empty;

        public static void ReadConfig()
        {
            if (Utills.CheckDir() == false)            
                Utills.CreateDir();

            using var reader = new StreamReader($"{Utills.PathToMainDir}\\MarketApp\\Config.txt");
            string? line;
            int i = 1;

            while ((line = reader.ReadLine()) != null)
            {
                if (i == 1)
                {
                    StemaId32 = line;
                    i++;
                }
                else if (i == 2)
                {
                    Steam_API_Key = line;
                    i++;
                }
                else if (i == 3)
                {
                    Market_API_Key = line;
                    i++;
                }
                else if (i == 4)
                {
                    Telegram_User_Id = line;
                }
            }

            if (Market_API_Key == string.Empty || StemaId32 == string.Empty || Steam_API_Key == string.Empty)
            {
                Notification.DisplayInfo("Entry Steam API or StemaId32 or Market API");
                System.Windows.Application.Current.Shutdown();
            }

        }
        public override string ToString()
        {
            return $"{StemaId32} \n{Steam_API_Key}\n{Market_API_Key}\n{Telegram_User_Id}";
        }
    }
}
