namespace MarketApp.Settings
{
    class Utills
    {
        public static void AddToAutoLoad()
        {
            try
            {
                string BaseDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                key.SetValue("MarketApp", System.IO.Path.Combine(BaseDir, "MarketApp.exe"));
            }
            catch { }
        }
        public static void RemoveFromAutoLoad()
        {
            try
            {
                string BaseDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                key.DeleteValue("MarketApp", false);
            }
            catch { }
        }
    }
}
