﻿using System.Threading.Tasks;
using Telegram.Bot;

namespace MarketBot.Notication
{
    public class Notification
    {
        public static string? Telegram_User_Id { get; set; }

        private const string _botKey = "5701818571:AAFTs8zmjlHqr3ZQHYC4Z5HNtse_3-f9jVA";

        public static async void TelegramNotification(string text)
        {
            var bot = new TelegramBotClient(_botKey);

            if (Telegram_User_Id != null)
                await bot.SendTextMessageAsync(Telegram_User_Id, text);
        }

        public static async void WindowNotification(string text)
        {
            var ni = new System.Windows.Forms.NotifyIcon
            {
                Icon = new System.Drawing.Icon("MarketApp.ico"),
                Visible = true,
                BalloonTipTitle = "Market App",
                BalloonTipText = text
            };
            ni.ShowBalloonTip(4000);
            await Task.Delay(4000);
            ni.Dispose();
        }
    }
}
