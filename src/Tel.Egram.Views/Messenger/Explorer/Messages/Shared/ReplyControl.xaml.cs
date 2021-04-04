﻿using Avalonia;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Smith.Model.Messenger.Explorer.Messages;

namespace Tel.Egram.Views.Messenger.Explorer.Messages.Shared
{
    public class ReplyControl : BaseControl<ReplyModel>
    {
        public ReplyControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
