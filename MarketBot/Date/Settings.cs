using MarketApp.Utills;
using MarketBot;
using Newtonsoft.Json;
using System.IO;

namespace MarketApp.Date
{
    public class Settings
    {

        public bool? AutoLoad { get; set; }
        public bool? AutoTray { get; set; }
        public bool? TelegramNotification { get; set; }
        public bool? WindowsNotification { get; set; }

        public static Settings ReadSettings()
        {
            Settings settingsInfo = new ();

            try
            {
                settingsInfo = JsonConvert.DeserializeObject<Settings>(File.ReadAllText($"{IOoperation.PathToMainDir}\\MarketApp\\Settings.json"));
            }

            catch{}

            return settingsInfo;
        }

        public static void ApplySettings(MainWindow mainWindow)
        {
            //Settings settingsInfo = Settings.ReadSettings();

            //if (settingsInfo.AutoLoad == true)
            //{
            //    Regedit.AddToAutoLoad();
            //}
            //else
            //{
            //    Regedit.RemoveFromAutoLoad();
            //}

            //if (settingsInfo.AutoTray == true)
            //{
            //    Tray.CloseToTray(mainWindow);
            //}
        }
    }

}
