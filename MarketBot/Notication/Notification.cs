using MarketBot.Date;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MarketBot.Notication
{
    public class Notification
    {
        public static async void TelegramNotication(string text)
        {
            var bot = new TelegramBotClient("5701818571:AAFTs8zmjlHqr3ZQHYC4Z5HNtse_3-f9jVA");
            await bot.SendTextMessageAsync(User_Date.Telegram_User_Id, text);
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
            ni.ShowBalloonTip(8000);
            await Task.Delay(8000);
            ni.Dispose();
        }
    }
}
