using MarketCore.Data;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketApp.Notification
{
    public static class WindowsNotification
    {
        public static async void WindowNotificationAsync(string text)
        {
            var settings = Settings.ReadSettings();

            if (settings.WindowsNotification != false)
            {
                var ni = new NotifyIcon
                {
                    Icon = new Icon(System.Windows.Application.GetResourceStream(
                    new Uri("pack://application:,,,/Resources/MarketApp.ico")).Stream),
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
    }
}
