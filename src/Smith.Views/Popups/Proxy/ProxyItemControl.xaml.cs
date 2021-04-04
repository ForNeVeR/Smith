using Avalonia.Markup.Xaml;
using Smith.Model.Settings.Proxy;

namespace Smith.Views.Popups.Proxy
{
    public class ProxyItemControl : BaseControl<ProxyModel>
    {
        public ProxyItemControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
