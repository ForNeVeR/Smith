using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Smith.Model.Settings;

namespace Tel.Egram.Views.Settings
{
    public class SettingsControl : BaseControl<SettingsModel>
    {
        public SettingsControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
