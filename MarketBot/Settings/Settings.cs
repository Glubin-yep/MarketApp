using Newtonsoft.Json;
using System.IO;

namespace MarketApp.Settings
{
    public class SettingsInfo
    {

        public bool? AutoLoad { get; set; }
        public bool? AutoTray { get; set; }
        public bool? TelegramNotification { get; set; }
        public bool? WindowsNotification { get; set; }

        public static SettingsInfo ReadSettings()
        {
            var settingsInfo = JsonConvert.DeserializeObject<SettingsInfo>(File.ReadAllText(@"Settings.json"));
            return settingsInfo;
        }
    }

}
