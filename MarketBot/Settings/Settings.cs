using MarketBot.Notication;
using MarketBot;
using Newtonsoft.Json;
using System;
using System.IO;

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
            var settingsInfo = JsonConvert.DeserializeObject<Settings>(File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}Settings.json"));
            return settingsInfo;
        }

        public static void ApplySettings(MainWindow mainWindow)
        {
            Settings settingsInfo = Settings.ReadSettings();

            if (settingsInfo.AutoLoad == true)
            {
                Utills.AddToAutoLoad();
            }
            else
            {
                Utills.RemoveFromAutoLoad();
            }

            if (settingsInfo.AutoTray == true)
            {
                Tray.CloseToTray(mainWindow);
            }
        }
    }

}
