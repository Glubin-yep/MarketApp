using MarketBot.Notication;
using MarketBot;
using Newtonsoft.Json;
using System;
using System.IO;
using MarketApp.Utills;

namespace MarketApp.Settings
{
    public class Settings
    {

        public bool? AutoLoad { get; set; }
        public bool? AutoTray { get; set; }
        public bool? TelegramNotification { get; set; }
        public bool? WindowsNotification { get; set; }

        public static Settings ReadSettings()
        {
            var settingsInfo = JsonConvert.DeserializeObject<Settings>(File.ReadAllText($"{IOoperation.PathToMainDir}\\MarketApp\\Settings.json"));
            return settingsInfo;
        }

        public static void ApplySettings(MainWindow mainWindow)
        {
            Settings settingsInfo = Settings.ReadSettings();

            if (settingsInfo.AutoLoad == true)
            {
                Regedit.AddToAutoLoad();
            }
            else
            {
                Regedit.RemoveFromAutoLoad();
            }

            if (settingsInfo.AutoTray == true)
            {
                Tray.CloseToTray(mainWindow);
            }
        }
    }

}
