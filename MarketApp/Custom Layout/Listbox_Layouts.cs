using System.Windows;
using System.Windows.Controls;


namespace MarketApp
{
    internal class LayoutListbox : ListBox
    {
        public Layout layout
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
