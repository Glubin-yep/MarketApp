using AdonisUI.Controls;
using MarketApp.Settings;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;

namespace MarketBot.Notication
{
    public static class Notification
    {
        public static string? Telegram_User_Id { get; set; }


        private const string _botKey = "5701818571:AAFTs8zmjlHqr3ZQHYC4Z5HNtse_3-f9jVA";

        public static async void TelegramNotificationAsync(string text)
        {
            var settings = SettingsInfo.ReadSettings();

            if (settings.TelegramNotification != false)
            {
                var bot = new TelegramBotClient(_botKey);

                if (Telegram_User_Id != null)
                    await bot.SendTextMessageAsync(Telegram_User_Id, text);
            }
        }

        public static async void WindowNotificationAsync(string text)
        {
            var settings = SettingsInfo.ReadSettings();

            if (settings.WindowsNotification != false)
            {
                var ni = new NotifyIcon
                {
                    Icon = new System.Drawing.Icon($"{AppDomain.CurrentDomain.BaseDirectory}MarketApp.ico"),
                    Visible = true,
                    BalloonTipTitle = "Market App",
                    BalloonTipText = text
                };
                ni.ShowBalloonTip(2000);
                await Task.Delay(2000);
                ni.Dispose();
            }
        }

        public static void DisplayInfo(string message)
        {
            AdonisUI.Controls.MessageBox.Show(message, "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static NotifyIcon CreateNoti()
        {
            var MyNotifyIcon = new NotifyIcon
            {
                Icon = new System.Drawing.Icon($"{AppDomain.CurrentDomain.BaseDirectory}MarketApp.ico")
            };
            return MyNotifyIcon;
        }
    }
}
