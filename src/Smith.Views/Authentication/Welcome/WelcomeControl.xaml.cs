using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Smith.Views.Authentication.Welcome
{
    public class WelcomeControl : UserControl
    {
        public WelcomeControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
