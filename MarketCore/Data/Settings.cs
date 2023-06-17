using MarketCore.Utills;
using Newtonsoft.Json;

namespace MarketCore.Data
{
    public class Settings
    {

        public bool AutoLoad { get; set; } = true;
        public bool AutoTray { get; set; } = true;
        public bool TelegramNotification { get; set; } = false;
        public bool WindowsNotification { get; set; } = true;

        public static Settings ReadSettings()
        {
            Settings settingsInfo = new();

            try
            {
                settingsInfo = JsonConvert.DeserializeObject<Settings>(File.ReadAllText($"{IOoperation.FullPathToData}\\Settings.json")) ?? new();
            }

            catch { }

            return settingsInfo;
        }
    }

}
