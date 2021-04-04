using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Smith.Model.Workspace.Navigation;

namespace Tel.Egram.Views.Workspace.Navigation
{
    public class NavigationControl : BaseControl<NavigationModel>
    {
        public NavigationControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
