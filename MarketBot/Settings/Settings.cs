using Newtonsoft.Json;
using System;
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
            var settingsInfo = JsonConvert.DeserializeObject<SettingsInfo>(File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}Settings.json"));
            return settingsInfo;
        }
    }

}
