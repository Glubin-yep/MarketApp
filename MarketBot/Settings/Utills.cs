using System;

namespace MarketApp.Settings
{
    public static class Utills
    {
        public static void AddToAutoLoad()
        {
            string BaseDir = AppDomain.CurrentDomain.BaseDirectory;

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            key.SetValue("MarketApp", System.IO.Path.Combine(BaseDir, "MarketApp.exe"));
        }
        public static void RemoveFromAutoLoad()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            key.DeleteValue("MarketApp", false);
        }
    }
}
