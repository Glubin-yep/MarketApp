using MarketApp.Date;
using System;
using System.IO;
using System.Text.Json;

namespace MarketApp.Utills
{
    public static class IOoperation
    {
        private static readonly string pathToMainDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}";
        public static string PathToMainDir { get => pathToMainDir; }

        public static bool CheckDir()
        {
            var dir = Directory.GetDirectories(PathToMainDir, "MarketApp");

            if (dir.Length == 0)
                return false;

            string[] files = Directory.GetFiles(pathToMainDir + "\\MarketApp");

            if (files.Length < 2)
                return false;

            if (files[0] != $"{PathToMainDir}\\MarketApp\\Config.txt")
                SaveConfig();


            if (files[1] != $"{PathToMainDir}\\MarketApp\\Settings.json")
                SaveSettings();


            return true;
        }

        public static void CreateDir()
        {
            Directory.CreateDirectory(PathToMainDir + "\\MarketApp");
            SaveConfig();
            SaveSettings();
        }

        public static void SaveConfig()
        {
            var conf = new Config().ToString();
            File.WriteAllText(PathToMainDir + "\\MarketApp\\Config.txt", Encryption.Encrypt(conf));
        }

        public static void SaveSettings()
        {
            var settings = Date.Settings.ReadSettings();
            var json = JsonSerializer.Serialize(settings);
            File.WriteAllText(PathToMainDir + "\\MarketApp\\Settings.json", json);
        }
        public static void SaveSettings(Date.Settings settings)
        {
            var json = JsonSerializer.Serialize(settings);
            File.WriteAllText(PathToMainDir + "\\MarketApp\\Settings.json", json);
        }
    }
}
