using Microsoft.Web.WebView2.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MarketBot.Pages
{
    /// <summary>
    /// Interaction logic for TablePage.xaml
    /// </summary>
    public partial class TablePage : Page
    {
        public TablePage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var webView2Environment = await CoreWebView2Environment.CreateAsync(null, "C:\\temp");
            await Browser.EnsureCoreWebView2Async(webView2Environment);
            Browser.Source = new Uri("https://skins-table.xyz/table_test_");
        }
    }
}
