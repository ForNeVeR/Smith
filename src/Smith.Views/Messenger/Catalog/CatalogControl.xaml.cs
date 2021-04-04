using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Catalog;

namespace Smith.Views.Messenger.Catalog
{
    public class CatalogControl : BaseControl<CatalogModel>
    {
        public CatalogControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
