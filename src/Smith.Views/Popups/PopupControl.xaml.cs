﻿using Avalonia.Markup.Xaml;
using Smith.Model.Popups;

namespace Smith.Views.Popups
{
    public class PopupControl : BaseControl<PopupModel>
    {
        public PopupControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
