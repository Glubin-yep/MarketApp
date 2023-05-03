using MarketCore.Data;
using System;
using System.IO;
using System.Text.Json;

namespace MarketCore.Utills
{
    public static class IOoperation
    {
        private static readonly string pathToMainDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}";
        public static string PathToMainDir { get => pathToMainDir; }

        private static readonly string fullPathToData = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\MarketApp";
        public static string FullPathToData { get => fullPathToData; }

        public static bool CheckDir()
        {
            var dir = Directory.GetDirectories(PathToMainDir, "MarketApp");

            if (dir.Length == 0)
                return false;

            string[] files = Directory.GetFiles(pathToMainDir + "\\MarketApp");

            if (files.Length < 2)
                return false;

            if (files[0] != $"{FullPathToData}\\Config.txt")
                SaveConfig();

            if (files[1] != $"{FullPathToData}\\Settings.json")
                SaveSettings();

            return true;
        }

        public static void CreateDir()
        {
            Directory.CreateDirectory(FullPathToData);
            SaveConfig();
            SaveSettings();
        }

        public static void SaveConfig()
        {
            var conf = new Config().ToString();
            File.WriteAllText(FullPathToData + "\\Config.txt", Encryption.Encrypt(conf));
        }

        public static void SaveSettings()
        {
            var settings = Settings.ReadSettings();
            var json = JsonSerializer.Serialize(settings);
            File.WriteAllText(FullPathToData + "\\Settings.json", json);
        }
        public static void SaveSettings(Settings settings)
        {
            var json = JsonSerializer.Serialize(settings);
            File.WriteAllText(FullPathToData + "\\Settings.json", json);
        }
    }
}
