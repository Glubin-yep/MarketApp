using System;
using System.IO;
using System.Text.Json;

namespace MarketApp.Settings
{
    public static class Utills
    {
        private static readonly string pathToMainDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}";
        public static string PathToMainDir { get => pathToMainDir; }

        public static void AddToAutoLoad()
        {
            string BaseDir = AppDomain.CurrentDomain.BaseDirectory;

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            key.SetValue("MarketApp", Path.Combine(BaseDir, "MarketApp.exe"));
        }
        public static void RemoveFromAutoLoad()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            key.DeleteValue("MarketApp", false);
        }
        public static bool CheckDir()
        {
            var dir = Directory.GetDirectories(PathToMainDir, "MarketApp");

            if (dir.Length == 0)
                return false;

            string[] files = Directory.GetFiles(pathToMainDir + "\\MarketApp");

            if (files.Length < 2)
                return false;

            if (files[0] != $"{PathToMainDir}\\MarketApp\\Config.txt")
            {
                CreateConfig();
            }

            if (files[1] != $"{PathToMainDir}\\MarketApp\\Settings.json")
            {
                CreateSettings();
            }

            return true;
        }

        public static void CreateDir()
        {
            Directory.CreateDirectory(PathToMainDir + "\\MarketApp");
            CreateConfig();
            CreateSettings();
        }

        private static void CreateConfig()
        {
            File.WriteAllText(PathToMainDir + "\\MarketApp\\Config.txt", new Config().ToString());
        }

        private static void CreateSettings()
        {
            var settings = new Settings();
            var json = JsonSerializer.Serialize(settings);
            File.WriteAllText(PathToMainDir + "\\MarketApp\\Settings.json", json);
        }
    }
}
