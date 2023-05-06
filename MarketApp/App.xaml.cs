﻿using System;
using System.Windows;

namespace MarketApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            DispatcherUnhandledException += (sender, e) =>
            {               
                e.Handled = true;
            };
        }
    }
}
