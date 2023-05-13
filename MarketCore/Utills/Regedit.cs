using System.Runtime.InteropServices;

namespace MarketCore.Utills
{
    public static class Regedit
    {
        public static void AddToAutoLoad()
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.ProcessPath != null)
                {
                    string exePath = Environment.ProcessPath;

                    Microsoft.Win32.RegistryKey? key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    key?.SetValue("MarketApp", exePath);
                }
            }
            catch { }
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
