using System.Text.Json.Serialization;

namespace MarketApp.Settings
{
    public class SettingsInfo
    {
        
        public  bool? AutoLoad { get; set; }
        public  bool? AutoTray { get; set; }
        public bool? TelegramNotification { get; set; }
        public bool? WindowsNotification { get; set; }
    }

}
