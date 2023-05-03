using MarketCore.Utills;
using Newtonsoft.Json;

namespace MarketCore.Data
{
    public class Settings
    {

        public bool? AutoLoad { get; set; }
        public bool? AutoTray { get; set; }
        public bool? TelegramNotification { get; set; }
        public bool? WindowsNotification { get; set; }

        public static Settings ReadSettings()
        {
            Settings settingsInfo = new();

            try
            {
                settingsInfo = JsonConvert.DeserializeObject<Settings>(File.ReadAllText($"{IOoperation.FullPathToData}\\Settings.json"));
            }

            catch { }

            return settingsInfo;
        }
    }

}
