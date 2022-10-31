using System.Windows.Controls;

namespace MarketBot.Parsing
{
    class CustomValidation
    {
        public static void Validation_TextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;
            string[] strings = ((TextBox)sender).Text.Split(".");

            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains('.'))
                    approvedDecimalPoint = true;
            }

            if (((TextBox)sender).Text.Contains('.'))
            {
                approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;

            if (strings.Length == 2 && strings[1].Length == 2 && approvedDecimalPoint == true)
                e.Handled = true;
        }
    }
}
