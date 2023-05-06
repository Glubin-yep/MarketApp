using MarketCore.API.MarketAPI;
using MarketCore.Utills;

namespace MarketCore.Data
{
    public class Config
    {
        public static string SteamId32 { get; set; } = string.Empty;
        public static string Steam_API_Key { get; set; } = string.Empty;
        public static string Market_API_Key { get; set; } = string.Empty;
        public static string Telegram_User_Id { get; set; } = string.Empty;

        public static bool ReadConfig()
        {
            if (IOoperation.CheckDir() == false)
                IOoperation.CreateDir();

            if (ChekConfig() == false)
                return false;

            return true;
        }

        public static bool ChekConfig()
        {
            try
            {
                var conf = Encryption.Decrypt(File.ReadAllText($"{IOoperation.FullPathToData}\\Config.txt"));
                var lines = conf.Split('\n');

                SteamId32 = lines[0];
                Steam_API_Key = lines[1];
                Market_API_Key = lines[2];
                Telegram_User_Id = lines[3];

                if (string.IsNullOrWhiteSpace(SteamId32) ||
                    string.IsNullOrWhiteSpace(Steam_API_Key) ||
                    string.IsNullOrWhiteSpace(Market_API_Key) )
                {
                    return false;
                }

                MarketAPI.Initialize(Market_API_Key);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"{SteamId32}\n{Steam_API_Key}\n{Market_API_Key}\n{Telegram_User_Id}\n";
        }
    }
}
