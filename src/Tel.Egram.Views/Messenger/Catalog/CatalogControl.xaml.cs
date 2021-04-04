﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Catalog;

namespace Tel.Egram.Views.Messenger.Catalog
{
    public class CatalogControl : BaseControl<CatalogModel>
    {
        public CatalogControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
