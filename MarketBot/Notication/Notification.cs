using MarketApp.Date;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Telegram.Bot;

namespace MarketBot.Notication
{
    public static class Notification
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

        public static async void WindowNotificationAsync(string text)
        {
            var settings = Settings.ReadSettings();

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
            AdonisUI.Controls.MessageBox.Show(message, "INFO", AdonisUI.Controls.MessageBoxButton.OK, AdonisUI.Controls.MessageBoxImage.Information);
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

    public static class Tray
    {
        private static readonly NotifyIcon myNotifyIcon = new()
        {
            Icon = new System.Drawing.Icon($"{AppDomain.CurrentDomain.BaseDirectory}MarketApp.ico")
        };

        public static NotifyIcon MyNotifyIcon { get => myNotifyIcon; }

        public static void CloseToTray(MainWindow mainWindow)
        {
            if (mainWindow.WindowState != WindowState.Minimized)
            {
                MyNotifyIcon.Visible = true;
                mainWindow.MainFrame.Source = null;
                mainWindow.ShowInTaskbar = false;
                Notification.WindowNotificationAsync("Application minimized to tray.");
                mainWindow.WindowState = WindowState.Minimized;
                mainWindow.Hide();
            }
        }

        public static void OpenFromTray(MainWindow mainWindow)
        {
            mainWindow.Show();
            mainWindow.WindowState = WindowState.Normal;
            myNotifyIcon.Visible = false;
            mainWindow.ShowInTaskbar = true;
        }

        public static void OpenContextMenuInTray(MainWindow mainWindow, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                myNotifyIcon.ContextMenuStrip = new ContextMenuStrip();
                myNotifyIcon.ContextMenuStrip.Items.Add("Exit");
                myNotifyIcon.ContextMenuStrip.Items[0].Click += (o, e) => { myNotifyIcon.Dispose(); mainWindow.Close(); };
            }
        }
    }
}
