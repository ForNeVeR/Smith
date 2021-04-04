using Avalonia.Markup.Xaml;
using Smith.Model.Settings.Proxy;

namespace Smith.Views.Popups.Proxy
{
    public class ProxyPopupControl : BaseControl<ProxyPopupContext>
    {
        public ProxyPopupControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
