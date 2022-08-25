using MarketBot.Date;
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
                        User_Date.Market_API_Key = line.Split(": ")[1];
                        i++;
                    }
                    else if (i == 2)
                    {
                        User_Date.StemaId32 = line.Split(": ")[1];
                        i++;
                    }
                    else if (i == 3)
                    {
                        User_Date.Steam_API_Key = line.Split(": ")[1];
                        i++;
                    }
                    else if (i == 4)
                    {
                        User_Date.Telegram_User_Id = line.Split(": ")[1];
                    }
                }
            }
        }
        public static string Get_Id_Name(string current_item, string mode)
        {
            string[] strings = current_item.Split(" /");
            if (mode == "name")
                return strings[0];

            return strings[^1];
        }
    }
}
