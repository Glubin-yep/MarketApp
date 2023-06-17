using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace MarketApp.Notification
{
    public static class TrayNotification
    {
        private static readonly NotifyIcon myNotifyIcon = new()
        {
            Icon = new Icon(System.Windows.Application.GetResourceStream(
                    new Uri("pack://application:,,,/Resources/MarketApp.ico")).Stream),
        };

        public static NotifyIcon MyNotifyIcon { get => myNotifyIcon; }

        public static void CloseToTray(MainWindow mainWindow)
        {
            if (mainWindow.WindowState != WindowState.Minimized)
            {
                MyNotifyIcon.Visible = true;
                mainWindow.MainFrame.Source = null;
                mainWindow.ShowInTaskbar = false;
                WindowsNotification.WindowNotificationAsync("Application minimized to tray.");
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

        public static void OpenContextMenuInTray(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                myNotifyIcon.ContextMenuStrip = new ContextMenuStrip();
                myNotifyIcon.ContextMenuStrip.Items.Add("Exit");
                myNotifyIcon.ContextMenuStrip.Items[0].Click += (o, e) => { myNotifyIcon.Dispose(); Environment.Exit(0); };
            }
        }
    }
}
