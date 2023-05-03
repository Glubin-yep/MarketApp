using MarketApp.Pages;
using MarketApp.Utills;
using MarketBot.Notication;
using System.IO;
using System.Threading.Tasks;

namespace MarketApp.Date
{
    public class Config
    {
        public static string SteamId32 { get; set; } = string.Empty;
        public static string Steam_API_Key { get; set; } = string.Empty;
        public static string Market_API_Key { get; set; } = string.Empty;
        public static string Telegram_User_Id { get; set; } = string.Empty;

        public static async Task<bool> ReadConfig()
        {
            if (IOoperation.CheckDir() == false)
                IOoperation.CreateDir();

            var conf = Encryption.Decrypt(File.ReadAllText($"{IOoperation.FullPathToData}\\Config.txt"));
            var lines = conf.Split('\n');

            SteamId32 = lines[0];
            Steam_API_Key = lines[1];
            Market_API_Key = lines[2];
            Telegram_User_Id = lines[3];

            if (ChekConfig() == false)
                await ReadConfig();

            MarketCore.MarketAPI.MarketAPI.Initialize(Market_API_Key);
            return true;
        }

        public static bool ChekConfig()
        {
            if (Market_API_Key == string.Empty || SteamId32 == string.Empty || Steam_API_Key == string.Empty)
            {
                Notification.DisplayInfo("Entry Steam API or StemaId32 or Market API");
                var entry = new ConfigPage();
                entry.ShowDialog();
            }
            return true;
        }

        public override string ToString()
        {
            return $"{SteamId32}\n{Steam_API_Key}\n{Market_API_Key}\n{Telegram_User_Id}\n";
        }
    }
}
