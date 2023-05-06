using System.Runtime.InteropServices;

namespace MarketCore.Utills
{
    public static class Regedit
    {
        public static void AddToAutoLoad()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string BaseDir = AppDomain.CurrentDomain.BaseDirectory;

                Microsoft.Win32.RegistryKey? key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                key?.SetValue("MarketApp", Path.Combine(BaseDir, "MarketApp.exe"));
            }
        }
        public static void RemoveFromAutoLoad()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Microsoft.Win32.RegistryKey? key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                key?.DeleteValue("MarketApp", false);
            }
        }
    }
}
