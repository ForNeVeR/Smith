﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Informer;

namespace Tel.Egram.Views.Messenger.Informer
{
    public class InformerControl : BaseControl<InformerModel>
    {
        public InformerControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
