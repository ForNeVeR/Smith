using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Smith.Views.Startup
{
    public class StartupControl : UserControl
    {
        public StartupControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
