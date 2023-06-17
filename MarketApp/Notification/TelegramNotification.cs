using MarketCore.Data;
using Telegram.Bot;

namespace MarketApp.Notification
{
    public static class TelegramNotification
    {
        private const string _botKey = "5701818571:AAFTs8zmjlHqr3ZQHYC4Z5HNtse_3-f9jVA";

        public static async void TelegramNotificationAsync(string text)
        {
            var settings = Settings.ReadSettings();

            if (settings.TelegramNotification != false)
            {
                var bot = new TelegramBotClient(_botKey);

                if (Config.Telegram_User_Id != null)
                    await bot.SendTextMessageAsync(Config.Telegram_User_Id, text);
            }
        }
    }

}
