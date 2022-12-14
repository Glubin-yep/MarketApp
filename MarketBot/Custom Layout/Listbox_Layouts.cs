using System.Windows;
using System.Windows.Controls;


namespace MarketBot
{
    internal class LayoutListbox : ListBox
    {
        public Layout Layout
        {
            get { return (Layout)GetValue(layoutProperty); }
            set { SetValue(layoutProperty, value); }
        }

        public static readonly DependencyProperty layoutProperty = DependencyProperty.Register("layout", typeof(Layout), typeof(LayoutListbox), new PropertyMetadata(Layout.Tile));

    }

    public enum Layout
    {
        Tile
    }
}
