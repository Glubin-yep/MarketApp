using MarketApp.Settings;
using MarketBot.API;
using MarketBot.Notication;
using System;
using System.IO;
using System.Windows;

namespace MarketBot.Parsing
{
    class ParseConfig
    {
        // TODO rewrite for json
        public static void ReadConfig()
        {
            using var reader = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}\\Config.txt");
            string line;
            int i = 1;
            while ((line = reader.ReadLine()) != null)
            {
                if (i == 1)
                {
                    MarketAPI.Market_API_Key = line.Split(": ")[1];
                    i++;
                }
                else if (i == 2)
                {
                    SteamAPI.StemaId32 = line.Split(": ")[1];
                    i++;
                }
                else if (i == 3)
                {
                    SteamAPI.Steam_API_Key = line.Split(": ")[1];
                    i++;
                }
                else if (i == 4)
                {
                    Notification.Telegram_User_Id = line.Split(": ")[1];
                }
            }
            if (MarketAPI.Market_API_Key == string.Empty || SteamAPI.StemaId32 == string.Empty || SteamAPI.Steam_API_Key == string.Empty)
            {
                Notification.DisplayInfo("Entry Steam API or StemaId32 or Market API");
                App.Current.Shutdown();
            }
        }

        public static void ApplySettings (MainWindow mainWindow, System.Windows.Forms.NotifyIcon notifyIcon)
        {
            SettingsInfo settingsInfo = SettingsInfo.ReadSettings();

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
                notifyIcon.Visible = true;
                mainWindow.MainFrame.Source = null;
                mainWindow.ShowInTaskbar = false;
                Notification.WindowNotificationAsync("Application minimized to tray.");
                mainWindow.WindowState = WindowState.Minimized;
                mainWindow.Hide();
            }
        }

        //TODO remove this shit
        public static string Get_Id_Name(string current_item, string mode)
        {
            string[] strings = current_item.Split(" /");
            if (mode == "name")
                return strings[0];

            return strings[^1];
        }
    }
}
